import AnswerChoice from "../../answer_choice/AnswerChoice"
import styles from "./AnswerSelect.module.css"
import React, { useEffect } from 'react'

export default function AnswerSelect({answers, type}) {

  return (
    <div className={styles.answersContainer}>
        {answers.map(answer => <AnswerChoice key={answer.id} type={type} answer={answer.name}/>)}
    </div>
  )
}
