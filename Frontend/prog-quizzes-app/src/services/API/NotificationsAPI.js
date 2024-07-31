import axios from "axios";

export default class NotificationsAPI { 
    static async getUserNotifications(user, page) {
        let result = await axios.get(`https://localhost:7184/api/notifications/${user.id}?page=${page}`);
        return result;
    }

    static async removeAllNotifications(user) {
        await axios.delete(`https://localhost:7184/api/notifications/clear/${user.id}`);
    }

    static async removeNotification(user, notificationId) {
        await axios.delete(`https://localhost:7184/api/notifications/${notificationId}/remove/${user.id}`);
    }
}