import useDifficulty from "../../../hooks/useDifficulty";
import useFetching from "../../../hooks/useFetching"
import ImageAPI from "../../../services/API/ImageAPI";
import Helper from "../../../services/Helper";
import Loading from "../../animations/Loading/Loading";
import GenericButton from "../../buttons/generic_button/GenericButton"
import CategoryDifficulty from "../../category_difficulty/CategoryDifficulty";
import SubcategoriesList from "../../subcategories_list/SubcategoriesList";
import styles from "./QuizIntro.module.css"
import React, { forwardRef, useEffect, useState } from 'react'

export default function QuizIntro({setIsIntro, quiz}) {

    const [image, setImage] = useState("");
    const [fetchImage, isLoading, isError] = useFetching(async () => {
        const imageData = await ImageAPI.getQuizImage(quiz.imageUrl);
        setImage(imageData);
  })
    const [parseDifficulty, difficulty] = useDifficulty(() => Helper.getDifficultyProperty(quiz.difficulty));

    useEffect(() => {
        fetchImage();
        parseDifficulty();
    }, [])

  return (
    <>
            <h1 className={styles.quizTitle}>{quiz.title}</h1>
            {isLoading ? <Loading/> : 
            <img width={700} height={500} className={styles.quizImage} src={`data:image;base64,${image}`} alt="Обложка викторины"></img>}
            <p className={styles.quizDescription}>{quiz.description}</p>
            <CategoryDifficulty categoryName={quiz.categoryName} difficulty={difficulty}/>
            <SubcategoriesList subcategories={quiz.subcategories}/>
            <GenericButton onClick={() => setIsIntro(false)}>Начать</GenericButton>
            <span className={styles.dateSpan}>{quiz.creationDate}</span>
     </>
  )
}
