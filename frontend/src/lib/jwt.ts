import type { Role } from "./types";

const CLAIM = "http://schemas.microsoft.com/ws/2008/06/identity/claims/";

export interface CurrentUser {
    id: number;
    username: string;
    role: Role;
}

function decodePayload(token: string): Record<string, unknown> | null {
    try {
        const part = token.split(".")[1];
        const base64 = part.replace(/-/g, "+").replace(/_/g, "/");
        const padded = base64.padEnd(Math.ceil(base64.length / 4) * 4, "=");
        const bytes = Uint8Array.from(atob(padded), (c) => c.charCodeAt(0));
        return JSON.parse(new TextDecoder().decode(bytes));
    } catch {
        return null;
    }
}

// ASP.NET may write the claim as "role" or as the long URL; accept either.
function readClaim(payload: Record<string, unknown>, short: string, long: string) {
    const value = payload[short] ?? payload[CLAIM + long];
    return typeof value === "string" ? value : null;
}

export function getUserFromToken(token: string): CurrentUser | null {
    const payload = decodePayload(token);
    if (!payload) return null;

    const id = readClaim(payload, "nameid", "nameidentifier");
    const username = readClaim(payload, "unique_name", "name");
    const role = readClaim(payload, "role", "role");

    if (!id || !username || !role) return null;

    return { id: Number(id), username, role: role as Role };
}