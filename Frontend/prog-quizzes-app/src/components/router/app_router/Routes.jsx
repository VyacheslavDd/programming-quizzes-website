
import MainPage from "../../../pages/main_page/MainPage";
import QuizPage from "../../../pages/quiz/QuizPage";
import QuizzesPage from "../../../pages/quizzes/QuizzesPage";
import { Provider } from "react-redux";
import { store } from "../../../redux/stores/QuizStore";
import QuizPageContainer from "../../../pages/quiz/QuizPageContainer";

export const routes = [
    {path: "/", element: MainPage, exact: true},
    {path: "/quizzes", element: QuizzesPage, exact: true},
    {path: "/quizzes/:id", element: QuizPageContainer, exact: true}
]