import React, { useEffect, useState } from 'react'
import QuizCard from '../../components/list_components/quiz_card/QuizCard'
import styles from "./QuizzesPage.module.css"
import Category from '../../components/category/Category'
import QuizList from '../../components/list_components/quiz_list/QuizList'
import axios from 'axios'
import QuizAPI from '../../services/API/QuizAPI'
import useFetching from '../../hooks/useFetching'
import Loading from '../../components/animations/Loading/Loading'
import ErrorMessage from '../../components/error_message/ErrorMessage'

export default function QuizzesPage() {

  const [quizzes, setQuizzes] = useState([]);
  const [fetchQuizzes, isLoadingQuizzes, isErrorOnLoadingQuizzes] = useFetching(async () => {
    const data = await QuizAPI.getAllAsync();
    setQuizzes(data);
  })

  useEffect(() => {
    fetchQuizzes();
  }, [])

  return (
    <div className={styles.outer}>
      <div className={styles.container}>
        {isLoadingQuizzes
        ? <Loading/>
        : isErrorOnLoadingQuizzes
        ? <ErrorMessage errorMsg="Не удалось загрузить викторины! Возвращайтесь позже..."/>
        : <QuizList quizzes={quizzes}/>}
      </div>
    </div>
  )
}
