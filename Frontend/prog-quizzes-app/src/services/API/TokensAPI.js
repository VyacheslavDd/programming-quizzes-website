import axios from "axios";

export default class TokensAPI {
    static async refreshTokens(userId) {
        let result = await axios.get(`https://localhost:7184/api/tokens/refresh/${userId}`);
        return result.data;
    }
}