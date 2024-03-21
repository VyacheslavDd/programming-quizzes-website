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
import PaginationRow from '../../components/pagination/pagination_row/PaginationRow'


export default function QuizzesPage() {

  const [quizzes, setQuizzes] = useState([]);

  const [searchQuery, setSearchQuery] = useState("");
  const [difficulty, setDifficulty] = useState(null);
  const [category, setCategory] = useState(null);
  const [subcategory, setSubcategory] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageLimit, setPageLimit] = useState(5);
  const [curPageLimit, setCurPageLimit] = useState(5);
  const [totalCount, setTotalCount] = useState(0);

  const [dateParameterSort, setDateParameterSort] = useState("");

  const [imagesInfo, setImagesInfo] = useState({});
  const [fetchQuizzes, isLoadingQuizzes, isErrorOnLoadingQuizzes] = useFetching(async () => {
    const [data, totalCount] = await QuizAPI.getQuizzesAsync(currentPage, pageLimit, category, subcategory, difficulty);
    setTotalCount(totalCount);
    setQuizzes(data);
  }, true)

  const onAccept = async () => {
    setCurrentPage(1);
    setCurPageLimit(pageLimit);
    await fetchQuizzes();
  }

  const onPaginationClick = (pageNumber) => {
    setCurrentPage(pageNumber);
  }

  useEffect(() => {
    fetchQuizzes();
  }, [currentPage])

  const filteredBySearchQuizzes = useSearchSorting(quizzes, searchQuery);
  const sortedQuizzes = useSortingByDate(filteredBySearchQuizzes, dateParameterSort);

  return (
    <div className={styles.outer}>
      <div className={styles.container}>
      <QuizFiltersBlock setSearchQuery={setSearchQuery}
          setDifficulty={setDifficulty} setCategory={setCategory} setSubcategory={setSubcategory}
          setLimit={setPageLimit} setDateParameterSort = {setDateParameterSort} onAccept={onAccept}/>
        {isLoadingQuizzes
        ? <Loading/>
        : isErrorOnLoadingQuizzes
        ? <ErrorMessage errorMsg="Не удалось загрузить викторины! Возвращайтесь позже..."/>
        : <>
          <QuizList imagesInfo={imagesInfo} quizzes={sortedQuizzes} setImagesInfo={setImagesInfo}/>
          <div className={styles.pagination}>
            <PaginationRow count={Math.ceil(totalCount / curPageLimit)} currentPage={currentPage - 1}
            onPaginationClick={(pageNumber) => onPaginationClick(pageNumber + 1)} showOnly={5}/>
          </div>
        </>}
      </div>
    </div>
  )
}
