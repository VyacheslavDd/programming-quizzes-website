import React from 'react'
import styles from "./QuizRating.module.css"

export default function QuizRating({value}) {
  return (
    <span className={styles.ratingSpan}>R: {value}</span>
  )
}
