import React from 'react'
import QuizCard from '../../components/list_components/quiz_card/QuizCard'
import styles from "./QuizzesPage.module.css"
import Category from '../../components/category/Category'

export default function QuizzesPage() {
  return (
    <div className={styles.container}>
      <QuizCard/>
    </div>
  )
}
