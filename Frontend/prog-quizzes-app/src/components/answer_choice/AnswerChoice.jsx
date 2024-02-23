import { useDispatch, useSelector } from "react-redux"
import GenericSelectInput from "../select_input_ui/GenericSelectInput"
import styles from "./AnswerChoice.module.css"
import React from 'react'
import { updateQuestionAnswersInfo } from "../../redux/slices/QuizSlice"

export default function AnswerChoice({type, answer}) {

  const quizDispatch = useDispatch();
  const currentQuestion = useSelector(state => state.currentQuestion);
  const questionAnswers = useSelector(state => state.questionAnswers);

  const updateChoicesInfo = (e) => {
    quizDispatch(updateQuestionAnswersInfo([currentQuestion, answer, type === "radio"]));
  }

  return (
    <div className={styles.choice}>
        <GenericSelectInput checked={questionAnswers[currentQuestion][answer]} onChange={(e) => updateChoicesInfo(e)}
         name="answer" type={type} value={answer}/>
        <span className={styles.answer}>{answer}</span>
    </div>
  )
}
