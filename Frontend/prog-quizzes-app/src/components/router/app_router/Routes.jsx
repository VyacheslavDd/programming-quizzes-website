
import MainPage from "../../../pages/main_page/MainPage";
import QuizPage from "../../../pages/quiz/QuizPage";
import QuizzesPage from "../../../pages/quizzes/QuizzesPage";
import { Provider } from "react-redux";
import { store } from "../../../redux/stores/QuizStore";
import QuizPageContainer from "../../../pages/quiz/QuizPageContainer";
import LoginPage from "../../../pages/login_page/LoginPage";
import RegistrationPage from "../../../pages/register_page/RegistrationPage";

export const defaultRoutes = [
    {path: "/", element: MainPage, exact: true},
    {path: "/quizzes", element: QuizzesPage, exact: true},
    {path: "/login", element: LoginPage, exact: true},
    {path: "/register", element: RegistrationPage, exact: true},
    {path: "*", element: LoginPage, exact: true}
]

export const authorizedRoutes = [
    {path: "/", element: MainPage, exact: true},
    {path: "/quizzes", element: QuizzesPage, exact: true},
    {path: "/quizzes/:id", element: QuizPageContainer, exact: true},
    {path: "*", element: MainPage, exact: true}
]