import GenericSelectInput from "../select_input_ui/GenericSelectInput"
import styles from "./AnswerChoice.module.css"
import React from 'react'

export default function AnswerChoice({type, answer}) {
  return (
    <div className={styles.choice}>
        <GenericSelectInput name="answer" type={type} value={answer}/>
        <span className={styles.answer}>{answer}</span>
    </div>
  )
}
