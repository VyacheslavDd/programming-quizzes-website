import React, { useEffect, useRef, useState } from 'react'
import styles from "./QuizCard.module.css"
import Category from '../../category/Category'
import Subcategory from '../../subcategory/Subcategory'
import Helper from '../../../services/Helper'
import useFetching from '../../../hooks/useFetching'
import ImageAPI from '../../../services/API/ImageAPI'
import Loading from '../../animations/Loading/Loading'
import { useNavigate } from 'react-router-dom'
import SubcategoriesList from '../../subcategories_list/SubcategoriesList'
import useDifficulty from '../../../hooks/useDifficulty'
import CategoryDifficulty from '../../category_difficulty/CategoryDifficulty'
import QuizRating from '../../quiz_rating/QuizRating'

export default function QuizCard({quiz, imagesInfo, setImagesInfo}) {

  const [parseDifficulty, difficulty] = useDifficulty(() => {
    return Helper.getDifficultyProperty(quiz.difficulty);
  });
  const [imageBytesRepr, setImageBytesRepr] = useState("");

  const [fetchImage, isLoading, isError] = useFetching(async () => {
    let imageBytes = await ImageAPI.getQuizImageAsync(quiz.imageUrl);
    setImagesInfo(prev => ({...prev, [quiz.imageUrl]: imageBytes}));
    setImageBytesRepr(imageBytes);
  });
  useEffect(() => {
    parseDifficulty();
    if (imagesInfo[quiz.imageUrl] === undefined) {
      fetchImage();
    }
    else {
      setImageBytesRepr(imagesInfo[quiz.imageUrl]);
    }
  }, [])

  const router = useNavigate();

  return (
    <div className={styles.cardBlock} onClick={() => router(`${quiz.id}`)}>
        <div className={styles.container}>
            {isLoading
            ? <Loading/>
            : <img width={230} height={200} style={{alignSelf: "center"}} className={styles.quizImage} src={`data:image;base64,${imageBytesRepr}`} alt='Изображение викторины'></img>}
            <h2>{quiz.title}</h2>
            <p className={styles.description}>{Helper.shortenQuizDescription(quiz.description)}</p>
            <CategoryDifficulty difficulty={difficulty} categoryName={quiz.categoryName}/>
            <QuizRating value={quiz.quizRatingsInfo.ratedByCount === 0 ? "Без оценки" : (quiz.quizRatingsInfo.ratingSum / quiz.quizRatingsInfo.ratedByCount).toFixed(2)}/>
            <SubcategoriesList subcategories={quiz.subcategories}/>
        </div>
    </div>
  )
}
