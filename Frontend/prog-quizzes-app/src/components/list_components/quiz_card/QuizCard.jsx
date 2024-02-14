import React, { useEffect, useRef, useState } from 'react'
import styles from "./QuizCard.module.css"
import Category from '../../category/Category'
import Subcategory from '../../subcategory/Subcategory'
import Helper from '../../../services/Helper'
import useFetching from '../../../hooks/useFetching'
import ImageAPI from '../../../services/API/ImageAPI'
import Loading from '../../animations/Loading/Loading'

export default function QuizCard({quiz}) {

  const [difficulty, setDifficulty] = useState("");
  const [imageBytesRepr, setImageBytesRepr] = useState("");
  const [fetchImage, isLoading, isError] = useFetching(async () => {
    let imageBytes = await ImageAPI.getImage(quiz.imageUrl);
    setImageBytesRepr(imageBytes);
  });
  console.log(quiz);
  useEffect(() => {
    setDifficulty(Helper.getDifficultyProperty(quiz.difficulty));
    fetchImage();
  }, [])

  return (
    <div className={styles.cardBlock}>
        <div className={styles.container}>
            {isLoading
            ? <Loading/>
            : <img width={230} height={200} className={styles.quizImage} src={`data:image;base64,${imageBytesRepr}`} alt='Изображение викторины'></img>}
            <h2>{quiz.title}</h2>
            <p className={styles.description}>{quiz.description}</p>
            <div className={styles.categoryDif}>
                <Category name={quiz.categoryName}/>
                <span className={styles.difSpan}>{difficulty}</span>
            </div>
            <div className={styles.subcategories}>
                {quiz.subcategories.map(subcategory => <Subcategory key={subcategory.id} name={subcategory.name}/>)}
            </div>
        </div>
    </div>
  )
}
