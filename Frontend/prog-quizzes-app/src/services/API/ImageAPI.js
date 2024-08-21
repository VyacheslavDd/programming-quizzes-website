import axios from "axios";

export default class ImageAPI {
    
    static async getQuizImageAsync(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/quizzes/${imageUrl}`);
        return response.data;
    }

    static async getQuestionImageAsync(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/questions/${imageUrl}`);
        return response.data;
    }

    static async getUserImageAsync(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/users/${imageUrl}`);
        return response.data;
    }
}