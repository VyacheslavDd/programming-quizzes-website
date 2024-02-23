import React, { useEffect, useRef, useState } from 'react'
import styles from "./QuizPage.module.css"
import { useParams } from 'react-router-dom'
import QuizIntro from '../../components/quiz_page_components/quiz_intro/QuizIntro'
import QuizAPI from '../../services/API/QuizAPI'
import useFetching from '../../hooks/useFetching'
import Loading from '../../components/animations/Loading/Loading'
import ErrorMessage from '../../components/error_message/ErrorMessage'
import GenericSelectInput from '../../components/select_input_ui/GenericSelectInput'
import AnswerChoice from '../../components/answer_choice/AnswerChoice'
import AnswerSelect from '../../components/quiz_page_components/answer_select/AnswerSelect'
import QuizQuestion from '../../components/quiz_page_components/quiz_question/QuizQuestion'
import PaginationButton from '../../components/pagination/pagination_button/PaginationButton'
import PaginationRow from '../../components/pagination/pagination_row/PaginationRow'
import ImageAPI from '../../services/API/ImageAPI'
import { useDispatch, useSelector } from 'react-redux'
import { setQuestionsCount, setCurrentQuestion, setQuestionAnswersInfo, resetState } from '../../redux/slices/QuizSlice'

export default function QuizPage() {

    const id = useParams()['id'];
    const [quiz, setQuiz] = useState({});
    const quizDispatch = useDispatch();
    const questionsCount = useSelector(state => state.questionsCount);
    const currentQuestion = useSelector(state => state.currentQuestion);
    const [fetchQuiz, isLoading, isError] = useFetching(async () => {
      const data = await QuizAPI.getQuiz(id);
      setQuiz(data);
      quizDispatch(setQuestionsCount(data.questions.length));
      quizDispatch(setQuestionAnswersInfo(data.questions));

    }, true);

    const [isIntro, setIsIntro] = useState(true);

    useEffect(() => {
      quizDispatch(resetState())
      fetchQuiz();
    }, [])

    return (
    <>
    <div className={styles.outer}>
      <div className={styles.wrapper}>
      {isLoading
    ? <Loading/>
    : isError ? <ErrorMessage errorMsg={"Не удалось загрузить викторину. Попробуйте позже!"}/>
    : isIntro
      ? <QuizIntro setIsIntro={setIsIntro} quiz={quiz}/>
      : <>
        <QuizQuestion question={quiz.questions[currentQuestion]}/>
        <PaginationRow count={questionsCount} currentPage={currentQuestion} onPaginationClick={(pageNumber) => {
          quizDispatch(setCurrentQuestion(pageNumber));
      }}/>
      </>
    }
      </div>
    </div>
    </>
  )
}
