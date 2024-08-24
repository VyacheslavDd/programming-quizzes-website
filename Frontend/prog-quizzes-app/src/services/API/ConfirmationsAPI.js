import axios from "axios";

export default class ConfirmationsAPI {
    static async confirm(sequence) {
        return (await axios.patch(`https://localhost:7184/api/confirmations/confirm?sequence=${sequence}`)).data;
    }
}