import PostPage from "../pages/PostPage";
import About from "../pages/about/About";
import Login from "../pages/login/Login";
import Posts from "../pages/posts/Posts";
import Standard from "../pages/standard/Standard";

export const PrivateRoutes = [
    {path: "/posts", element: Posts, exact: true},
    {path: "/posts/:id", element: PostPage, exact: true},
    {path: "/", element: Standard, exact: true},
    {path: "/about", element: About, exact: true},
]
export const PublicRoutes = [
    {path: "/", element: Standard, exact: true},
    {path: "/about", element: About, exact: true},
    {path: "/login", element: Login, exact: true},
]