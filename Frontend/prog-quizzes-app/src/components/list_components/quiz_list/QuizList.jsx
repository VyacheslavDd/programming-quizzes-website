import React from 'react'
import styles from "./QuizList.module.css"
import QuizCard from '../quiz_card/QuizCard'

export default function QuizList({quizzes, imagesInfo, setImagesInfo}) {
  return (
    <ul className={styles.quizList}>
        {quizzes.map(curQuiz => <li key={curQuiz.id}>
            <QuizCard imagesInfo={imagesInfo} setImagesInfo={setImagesInfo}
             quiz={{id: curQuiz.id, title: curQuiz.title, description: curQuiz.description,
    difficulty: curQuiz.difficulty, imageUrl: curQuiz.imageUrl, categoryName: curQuiz.categoryName,
     subcategories: curQuiz.subcategories, quizRatingsInfo: curQuiz.quizRatingsInfo}}/>
            </li>)}
    </ul>
  )
}
