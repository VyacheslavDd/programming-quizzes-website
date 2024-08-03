import axios from "axios";

export default class NotificationsAPI { 
    static async getUserNotifications(userId, page) {
        let result = await axios.get(`https://localhost:7184/api/notifications/${userId}?page=${page}`);
        return result;
    }

    static async removeAllNotifications(userId) {
        await axios.delete(`https://localhost:7184/api/notifications/clear/${userId}`);
    }

    static async removeNotification(userId, notificationId) {
        await axios.delete(`https://localhost:7184/api/notifications/${notificationId}/remove/${userId}`);
    }
}