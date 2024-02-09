import About from "../../about/About";
import MainPage from "../../main_page/MainPage";

export const routes = [
    {path: "/", element: MainPage, exact: true},
    {path: "about", element: About, exact: true}
]