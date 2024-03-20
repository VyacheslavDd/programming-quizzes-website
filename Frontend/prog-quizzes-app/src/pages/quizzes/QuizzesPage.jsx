import React, { useEffect, useMemo, useState } from 'react'
import QuizCard from '../../components/list_components/quiz_card/QuizCard'
import styles from "./QuizzesPage.module.css"
import Category from '../../components/category/Category'
import QuizList from '../../components/list_components/quiz_list/QuizList'
import axios from 'axios'
import QuizAPI from '../../services/API/QuizAPI'
import useFetching from '../../hooks/useFetching'
import Loading from '../../components/animations/Loading/Loading'
import ErrorMessage from '../../components/error_message/ErrorMessage'
import QuizFiltersBlock from '../../components/quiz_filters_block/QuizFiltersBlock'
import Helper from '../../services/Helper'
import { useFilterSorting, useSearchSorting, useSortingByDate } from '../../hooks/useQuizSorting'

export default function QuizzesPage() {

  const [quizzes, setQuizzes] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [difficulty, setDifficulty] = useState(0);
  const [category, setCategory] = useState("");
  const [subcategory, setSubcategory] = useState("");
  const [dateParameterSort, setDateParameterSort] = useState("");
  const [imagesInfo, setImagesInfo] = useState({});
  const [fetchQuizzes, isLoadingQuizzes, isErrorOnLoadingQuizzes] = useFetching(async () => {
    const data = await QuizAPI.getAllAsync();
    setQuizzes(data);
  }, true)

  useEffect(() => {
    fetchQuizzes();
  }, [])

  const filteredBySearchQuizzes = useSearchSorting(quizzes, searchQuery);
  const filteredByFiltersQuizzes = useFilterSorting(filteredBySearchQuizzes, difficulty, category, subcategory);
  const sortedQuizzes = useSortingByDate(filteredByFiltersQuizzes, dateParameterSort);

  return (
    <div className={styles.outer}>
      <div className={styles.container}>
        {isLoadingQuizzes
        ? <Loading/>
        : isErrorOnLoadingQuizzes
        ? <ErrorMessage errorMsg="Не удалось загрузить викторины! Возвращайтесь позже..."/>
        : <>
          <QuizFiltersBlock setSearchQuery={setSearchQuery}
          setDifficulty={setDifficulty} setCategory={setCategory} setSubcategory={setSubcategory}
          setDateParameterSort = {setDateParameterSort}/>
          <QuizList imagesInfo={imagesInfo} quizzes={sortedQuizzes} setImagesInfo={setImagesInfo}/>
        </>}
      </div>
    </div>
  )
}
