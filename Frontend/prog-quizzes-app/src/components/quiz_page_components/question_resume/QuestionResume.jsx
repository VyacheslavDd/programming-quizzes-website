import styles from "./QuestionResume.module.css"
import React from 'react'

export default function QuestionResume({question, isCorrect}) {
  console.log(isCorrect);
  return (
    <div className={styles.resumeBlock}
    style={{backgroundColor: (!isCorrect ? "rgb(240, 127, 127)" : "rgb(121, 245, 152)")}}>
        QuestionResume
      </div>
  )
}
