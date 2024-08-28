import axios from "axios";

export default class PasswordRequestsAPI {
    static async sendRequest(email) {
        return (await axios.post("https://localhost:7184/api/password-requests/send", {
            email
        })).data;
    }

    static async getRequest(sequence) {
        return (await axios.get(`https://localhost:7184/api/password-requests/${sequence}`)).data;
    }
}