import axios from "axios";

export default class CategoryAPI {
    static async GetAllAsync() {
        let categories = await axios.get("https://localhost:7184/api/categories/all");
        return categories.data;
    }

    static async GetConnectedSubcategoriesAsync(id) {
        let subCategories = await axios.get(`https://localhost:7184/api/categories/${id}/subcategories`);
        return subCategories.data;
    }
}