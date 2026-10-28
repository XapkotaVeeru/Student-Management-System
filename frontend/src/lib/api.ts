import { tokenStore } from "./tokenStore";
import type { ApiResponse, LoginResponse } from "./types";

const API_URL: string = import.meta.env.VITE_API_URL ?? "";

if (!API_URL) {
    throw new Error("VITE_API_URL is not set. Copy .env.example to .env.");
}

export class ApiError extends Error {
    status: number;

    constructor(message: string, status: number) {
        super(message);
        this.name = "ApiError";
        this.status = status;
    }
}

export function saveSession(data: LoginResponse) {
    tokenStore.setAccessToken(data.accessToken);
    tokenStore.setRefreshToken(data.refreshToken);
}

// --- refresh, shared so two callers never refresh at the same time ---
let refreshInFlight: Promise<boolean> | null = null;

export function refreshSession(): Promise<boolean> {
    if (!refreshInFlight) {
        refreshInFlight = doRefresh().finally(() => {
            refreshInFlight = null;
        });
    }
    return refreshInFlight;
}

async function doRefresh(): Promise<boolean> {
    const refreshToken = tokenStore.getRefreshToken();
    if (!refreshToken) return false;

    try {
        const res = await fetch(`${API_URL}/api/Auth/refresh-token`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken }),
        });
        const body = (await res.json()) as ApiResponse<LoginResponse>;

        if (!res.ok || !body.status || !body.data) {
            tokenStore.clear(); // the server rejected it: the session is over
            return false;
        }

        saveSession(body.data); // rotation: always keep the NEW refresh token
        return true;
    } catch {
        return false; // network problem: keep the tokens so the user can retry
    }
}

// --- the main request function ---
function send(path: string, options: RequestInit): Promise<Response> {
    const headers = new Headers(options.headers);

    if (options.body && !headers.has("Content-Type")) {
        headers.set("Content-Type", "application/json");
    }

    const token = tokenStore.getAccessToken();
    if (token) headers.set("Authorization", `Bearer ${token}`);

    return fetch(`${API_URL}${path}`, { ...options, headers });
}

async function readBody(response: Response): Promise<Partial<ApiResponse<unknown>> & { title?: string } | null> {
    try {
        return await response.json();
    } catch {
        return null; // empty or non-JSON body
    }
}

function friendlyMessage(status: number, body: { message?: string; title?: string } | null) {
    if (body?.message) return body.message;
    if (status === 401) return "Your session has expired. Please log in again.";
    if (status === 403) return "You do not have permission to do this.";
    if (body?.title) return body.title;
    return `Request failed (${status})`;
}

export async function apiRequest<T>(path: string, options: RequestInit = {}): Promise<T> {
    let response = await send(path, options);

    // Expired access token: refresh once, then retry the same request.
    if (response.status === 401 && tokenStore.getRefreshToken()) {
        if (await refreshSession()) {
            response = await send(path, options);
        }
    }

    const body = await readBody(response);

    if (!response.ok || body?.status === false) {
        throw new ApiError(friendlyMessage(response.status, body), response.status);
    }

    return body?.data as T;
}