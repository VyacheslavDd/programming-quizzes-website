
import MainPage from "../../../pages/main_page/MainPage";
import QuizPage from "../../../pages/quiz/QuizPage";
import QuizzesPage from "../../../pages/quizzes/QuizzesPage";
import { Provider } from "react-redux";
import { store } from "../../../redux/stores/QuizStore";
import QuizPageContainer from "../../../pages/quiz/QuizPageContainer";
import LoginPage from "../../../pages/login_page/LoginPage";
import RegistrationPage from "../../../pages/register_page/RegistrationPage";
import CabinetPage from "../../../pages/user_cabinet/CabinetPage";

export const defaultRoutes = [
    {path: "/", element: MainPage},
    {path: "/quizzes", element: QuizzesPage},
    {path: "/login", element: LoginPage},
    {path: "/register", element: RegistrationPage},
    {path: "*", element: LoginPage}
]

export const authorizedRoutes = [
    {path: "/", element: MainPage},
    {path: "/quizzes", element: QuizzesPage},
    {path: "/quizzes/:id", element: QuizPageContainer},
    {path: "/cabinet", element: CabinetPage},
    {path: "*", element: MainPage},
]