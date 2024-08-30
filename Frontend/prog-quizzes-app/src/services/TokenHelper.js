import { jwtDecode } from "jwt-decode";
import TokensAPI from "./API/TokensAPI";

export default class TokenHelper {

    static tokenStorageKey = "token";

    static isTokenInvalid(token) {
        try {
            let decodedToken = jwtDecode(token);
            return false;
        }
        catch {
            return true;
        }
    }

    static isTokenExpired(token) {
        let decodedToken = jwtDecode(token);
        if (decodedToken['exp'] === undefined) return true;
        let expirationTime = decodedToken['exp'] * 1000;
        return Date.now() > expirationTime;
    }

    static async tryRefreshToken(updateToken) {
        let rawToken = this.getRawToken();
        try {
            if (this.isTokenInvalid(rawToken)) throw new Error();
            if (this.isTokenExpired(rawToken)) {
                let decodedToken = jwtDecode(rawToken);
                let result = await TokensAPI.refreshTokens(decodedToken['Id']);
                if (result.accessToken === "") throw new Error();
                localStorage.setItem(this.tokenStorageKey, result.accessToken);
                updateToken(result.accessToken);
                }
        }
            catch {
                localStorage.removeItem(TokenHelper.tokenStorageKey);
                updateToken("");
            }
    }

    static getReadyToken() {
        return `bearer ${localStorage.getItem(this.tokenStorageKey)}`;
    }

    static getRawToken() {
        return localStorage.getItem(this.tokenStorageKey);
    }
}