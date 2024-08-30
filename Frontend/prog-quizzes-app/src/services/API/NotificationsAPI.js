import axios from "axios";
import Helper from "../Helper";
import TokenHelper from "../TokenHelper";

export default class NotificationsAPI { 
    static async getUserNotifications(userId, page, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        let result = await axios.get(`https://localhost:7184/api/notifications/${userId}?page=${page}`, {headers: {Authorization: TokenHelper.getReadyToken()}});
        return result;
    }

    static async removeAllNotifications(userId, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.delete(`https://localhost:7184/api/notifications/clear/${userId}`, {headers: {Authorization:TokenHelper.getReadyToken()}});
    }

    static async removeNotification(userId, notificationId, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.delete(`https://localhost:7184/api/notifications/${notificationId}/remove/${userId}`, {headers: {Authorization: TokenHelper.getReadyToken()}});
    }
}