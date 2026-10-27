const REFRESH_KEY = "sms.refreshToken";

let accessToken: string | null = null;

export const tokenStore = {
    getAccessToken: () => accessToken,
    setAccessToken: (token: string | null) => {
        accessToken = token;
    },

    getRefreshToken: () => localStorage.getItem(REFRESH_KEY),
    setRefreshToken: (token: string) => localStorage.setItem(REFRESH_KEY, token),

    clear: () => {
        accessToken = null;
        localStorage.removeItem(REFRESH_KEY);
    },
};