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

export default function QuizPage() {

    const id = useParams()['id'];
    const [quiz, setQuiz] = useState({});
    const [images, setImages] = useState({});
    const [currentQuestion, setCurrentQuestion] = useState(0);

    const [fetchQuiz, isLoading, isError] = useFetching(async () => {
      const data = await QuizAPI.getQuiz(id);
      setQuiz(data);
    }, true);
    const [fetchImages, areImagesLoading, areImagesError] = useFetching(async () => {
      let counter = 0;
      for (let question of quiz.questions) {
        let data = await ImageAPI.getQuestionImage(question.imageUrl);
        setImages(prevImages => ({...prevImages, [counter - 1]: data}));
        counter++;
      }
  }, true);
    const [isIntro, setIsIntro] = useState(true);

    useEffect(() => {
      fetchQuiz();
    }, [])

    useEffect(() => {
      fetchImages();
    }, [quiz])

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
        <QuizQuestion question={quiz.questions[currentQuestion]} counterState={setCurrentQuestion}
        image={images[currentQuestion]} currentQuestionId={currentQuestion}/>
        <PaginationRow count={quiz.questions.length} currentPage={currentQuestion} onPaginationClick={(pageNumber) => {
          setCurrentQuestion(pageNumber);
      }}/>
      </>
    }
      </div>
    </div>
    </>
  )
}
