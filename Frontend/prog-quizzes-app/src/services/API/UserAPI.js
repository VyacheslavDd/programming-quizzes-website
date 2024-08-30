import axios from "axios";
import Helper from "../Helper";
import TokenHelper from "../TokenHelper";

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
        return result.data;
    }

    static async updatePassword(guid, passwordData, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        let result = await axios.patch(`https://localhost:7184/api/users/update/${guid}/password`, {
            previousPassword: passwordData.oldPassword,
            newPassword: passwordData.newPassword
        }, {headers: {Authorization: TokenHelper.getReadyToken()}});
        return result.data;
    }

    static async resetPassword(guid, sequence, password, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        return (await axios.patch(`https://localhost:7184/api/users/reset-password/${guid}?sequence=${sequence}`, {
            password
        }, {headers: {Authorization: TokenHelper.getReadyToken()}})).data;
    }

    static async getUserData(guid, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        return await axios.get(`https://localhost:7184/api/users/${guid}`, {headers: {Authorization: TokenHelper.getReadyToken()}});
    }

    static async updateUser(user, userId, file, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        const formData = new FormData();
        for (let entry of Object.entries(user)) {
            if (entry[0] !== "imageUrl") {
                formData.append(`userInfo.${entry[0]}`, entry[1]);
            }
        }
        formData.append("avatar", file);
        let result = await axios.put(`https://localhost:7184/api/users/update/${userId}`, formData, {headers: {Authorization: TokenHelper.getReadyToken()}});
        return result.data;
    }

    static async updateReceiveNotificationsOption(userId, value, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        let result = await axios.patch(`https://localhost:7184/api/users/update/${userId}/notifications`, {
            receiveNotifications: value
        }, {headers: {Authorization: TokenHelper.getReadyToken()}});
        return result.data;
    }

    static async clearNewNotificationsCount(userId, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.patch(`https://localhost:7184/api/users/clear/${userId}/notifications-count`, undefined, {headers: {Authorization: TokenHelper.getReadyToken()}});
    }
}