import axios from "axios";

export default class UserAPI {
    static async authenticate(loginOrEmail, password) {
        let result = await axios.post("https://localhost:7184/api/auth/authenticate",
            {
                loginOrEmail,
                password
            }
        );
        return result.data;
    }

    static async register(email, login, password) {
        let result = await axios.post("https://localhost:7184/api/auth/register",
            {
                email,
                login,
                password
            }
        );
        console.log(email, login, password);
        return result.data;
    }

    static async updatePassword(guid, passwordData) {
        let result = await axios.patch(`https://localhost:7184/api/users/update/${guid}/password`, {
            previousPassword: passwordData.oldPassword,
            newPassword: passwordData.newPassword
        });
        return result.data;
    }

    static async getUserData(guid) {
        return await axios.get(`https://localhost:7184/api/users/${guid}`);
    }

    static async updateUser(user, userId) {
        console.log(user, userId);
        let result = await axios.put(`https://localhost:7184/api/users/update/${userId}`, {
            userInfo: {
                name: user.name,
                surname: user.surname,
                birthDate: user.birthDate,
                phoneNumber: user.phoneNumber,
                email: user.email,
                login: user.login
            }
        });
        return result.data;
    }

    static async updateReceiveNotificationsOption(userId, value) {
        let result = await axios.patch(`https://localhost:7184/api/users/update/${userId}/notifications`, {
            receiveNotifications: value
        });
        return result.data;
    }

    static async clearNewNotificationsCount(userId) {
        await axios.patch(`https://localhost:7184/api/users/clear/${userId}/notifications-count`);
    }
}