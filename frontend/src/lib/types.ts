export type Role = "SuperAdmin" | "Admin" | "Teacher" | "Student";

// Mirrors your ResponseDto<T> { Status, Message, Data }
export interface ApiResponse<T> {
    status: boolean;
    message: string;
    data: T | null;
}

// Mirrors LoginResponseDto
export interface LoginResponse {
    accessToken: string;
    refreshToken: string;
    expiresAt: string;
}