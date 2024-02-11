import React, { useEffect, useRef, useState } from 'react'
import styles from "./QuizCard.module.css"
import Category from '../../category/Category'
import Subcategory from '../../subcategory/Subcategory'
import Helper from '../../../services/Helper'

export default function QuizCard({quiz}) {

  const [difficulty, setDifficulty] = useState("");

  useEffect(() => {
    setDifficulty(Helper.getDifficultyProperty(quiz.difficulty));
  }, [])

  return (
    <div className={styles.cardBlock}>
        <div className={styles.container}>
            <img className={styles.quizImage} src='https://placehold.co/230x200' alt='Изображение викторины'></img>
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
