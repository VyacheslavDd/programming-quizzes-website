import axios from "axios";

export default class UserAPI {
    static async authenticate(loginOrEmail, password) {
        let result = await axios.post("https://localhost:7063/api/auth/authenticate",
            {
                loginOrEmail,
                password
            }
        );
        return result.data;
    }

    static async register(email, login, password) {
        let result = await axios.post("https://localhost:7063/api/auth/register",
            {
                email,
                login,
                password
            }
        );
        return result.data;
    }
}