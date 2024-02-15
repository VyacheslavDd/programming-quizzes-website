
import MainPage from "../../../pages/main_page/MainPage";
import QuizPage from "../../../pages/quiz/QuizPage";
import QuizzesPage from "../../../pages/quizzes/QuizzesPage";

export const routes = [
    {path: "/", element: MainPage, exact: true},
    {path: "/quizzes", element: QuizzesPage, exact: true},
    {path: "/quizzes/:id", element: QuizPage, exact: true}
]