import axios from "axios";

export default class ImageAPI {
    static async getImage(imageUrl) {
        const response = await axios.get(`https://localhost:7184/api/images/${imageUrl}`);
        console.log(response.data);
        return response.data;
    }
}