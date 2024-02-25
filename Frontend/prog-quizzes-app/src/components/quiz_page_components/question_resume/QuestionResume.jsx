import { useSelector } from "react-redux"
import styles from "./QuestionResume.module.css"
import React from 'react'
import AnswerResume from "../answer_resume/AnswerResume";

export default function QuestionResume({question, questionIndex, isCorrect}) {

  const questionAnswers = useSelector(state => state.questionAnswers);

  return (
    <div className={styles.resumeBlock}
    style={{backgroundColor: (!isCorrect ? "rgb(240, 127, 127)" : "rgb(121, 245, 152)")}}>
        <p className={styles.question}>{question.title}</p>
        <ul className={styles.answersList}>
          {question.answers.map(answer => <li className={styles.answer} key={answer.id}><AnswerResume
          name={answer.name} userChoice={questionAnswers[questionIndex][answer.name]} actualChoice={answer.isCorrect}/></li>)}
        </ul>
        <p className={styles.info}>{isCorrect ? question.successInfo : question.failureInfo}</p>
      </div>
  )
}
