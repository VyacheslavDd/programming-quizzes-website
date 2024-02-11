import React from 'react'
import styles from "./QuizList.module.css"
import QuizCard from '../quiz_card/QuizCard'

export default function QuizList({quizzes}) {
  return (
    <ul className={styles.quizList}>
        {quizzes.map(curQuiz => <li key={curQuiz.id}>
            <QuizCard quiz={{title: curQuiz.title, description: curQuiz.description,
    difficulty: curQuiz.difficulty, categoryName: curQuiz.categoryName,
     subcategories: curQuiz.subcategories}}/>
            </li>)}
    </ul>
  )
}
