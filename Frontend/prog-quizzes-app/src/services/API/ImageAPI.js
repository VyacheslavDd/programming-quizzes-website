import axios from "axios";

export default class ImageAPI {
    
    static async getQuizImageAsync(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/quiz/${imageUrl}`);
        return response.data;
    }
    static async getQuestionImageAsync(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/question/${imageUrl}`);
        return response.data;
    }
}