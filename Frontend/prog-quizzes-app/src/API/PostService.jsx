import axios from "axios";


export default class PostService {
    static async getPage(params) {
        const queryParams = new URLSearchParams(params);
        const response = await axios.get(`https://jsonplaceholder.typicode.com/posts?${queryParams}`);
        const posts = [];
        for (let post of response.data) {
            posts.push({id: post.id, title: post.title, body: post.body});
        }
        return [posts, Number(response.headers['x-total-count'])];
    }

    static async getPostById(id) {
        const response = await axios.get(`https://jsonplaceholder.typicode.com/posts/${id}`);
        return response.data;
    }

    static async getCommentsToPost(id) {
        const response = await axios.get(`https://jsonplaceholder.typicode.com/posts/${id}/comments`);
        return response.data;
    }
}