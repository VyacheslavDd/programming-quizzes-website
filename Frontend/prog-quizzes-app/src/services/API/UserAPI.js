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

    static async updatePassword(guid, passwordData) {
        let result = await axios.patch(`https://localhost:7063/api/users/update/${guid}/password`, {
            previousPassword: passwordData.oldPassword,
            newPassword: passwordData.newPassword
        });
        return result.data;
    }

    static async getUserData(guid) {
        return await axios.get(`https://localhost:7063/api/users/${guid}`);
    }

    static async updateUser(user) {
        let result = await axios.put(`https://localhost:7063/api/users/update/${user.id}`, {
            name: user.name,
            surname: user.surname,
            birthDate: user.birthDate,
            phoneNumber: user.phoneNumber,
            email: user.email,
            login: user.login
        });
        return result.data;
    }
}