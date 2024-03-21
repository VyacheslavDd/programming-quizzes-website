import axios, { Axios } from "axios";

export default class QuizAPI {
    static async getAllAsync() {
        const response = await axios.get("https://localhost:7184/api/quizzes/all");
        return response.data;
    }

    static async getQuizAsync(id) {
        const response = await axios.get(`https://localhost:7184/api/quizzes/${id}`);
        return response.data;
    }

    static async getQuizzesAsync(page, limit, categoryId=null, subcategoryId=null, difficulty=null) {
        let config = {
            params: {
                page: page,
                limit: limit,
                categoryId: categoryId,
                subcategoryId: subcategoryId,
                difficulty: difficulty
            }
        }
        
        const response = await axios.get("https://localhost:7184/api/quizzes", config);
        return [response.data, response.headers["content-count"]];
    }
}