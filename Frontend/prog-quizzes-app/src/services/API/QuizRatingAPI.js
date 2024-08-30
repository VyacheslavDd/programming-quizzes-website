import axios from "axios";
import Helper from "../Helper";
import TokenHelper from "../TokenHelper";

export default class QuizRatingAPI {
    static async getUserRatingAsync(quizId, userId) {
        let result = await axios.get(`https://localhost:7184/api/quiz-ratings?userId=${userId}&quizId=${quizId}`);
        return result.data;
    }

    static async removeUserRatingAsync(quizId, userId, rating, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.delete(`https://localhost:7184/api/quiz-ratings/remove?userId=${userId}&quizId=${quizId}&rating=${rating}`,
            {headers: {Authorization: TokenHelper.getReadyToken()}}
        )
    }  

    static async addUserRatingAsync(quizId, userId, rating, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.post(`https://localhost:7184/api/quiz-ratings/rate`, {
            quizId,
            userId,
            rating
        },{headers: {Authorization: TokenHelper.getReadyToken()}});
    }

    static async updateUserRatingAsync(quizId, userId, rating, updateToken) {
        await TokenHelper.tryRefreshToken(updateToken);
        await axios.patch(`https://localhost:7184/api/quiz-ratings/update`, {
            quizId,
            userId,
            rating
        }, {headers: {Authorization: TokenHelper.getReadyToken()}});
    }
}