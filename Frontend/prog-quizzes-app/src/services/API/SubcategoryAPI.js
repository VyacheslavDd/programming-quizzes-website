import axios from "axios";

export default class SubcategoryAPI {
    static async GetAllAsync() {
        let subcategories = await axios.get("https://localhost:7184/api/subcategories/all");
        return subcategories.data;
    }
}