import axios from "axios";

export default class QuizRatingAPI {
    static async getUserRatingAsync(quizId, userId) {
        let result = await axios.get(`https://localhost:7184/api/quiz-ratings?userId=${userId}&quizId=${quizId}`);
        return result.data;
    }

    static async removeUserRatingAsync(quizId, userId, rating) {
        await axios.delete(`https://localhost:7184/api/quiz-ratings/remove?userId=${userId}&quizId=${quizId}&rating=${rating}`)
    }  

    static async addUserRatingAsync(quizId, userId, rating) {
        await axios.post(`https://localhost:7184/api/quiz-ratings/rate`, {
            quizId,
            userId,
            rating
        });
    }

    static async updateUserRatingAsync(quizId, userId, rating) {
        await axios.patch(`https://localhost:7184/api/quiz-ratings/update`, {
            quizId,
            userId,
            rating
        });
    }
}