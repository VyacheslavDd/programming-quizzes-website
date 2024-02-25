import styles from "./AnswerResume.module.css"
import React from 'react'

export default function AnswerResume({name, userChoice, actualChoice}) {

  return (
    <>
        <span className={styles.resume} style={{textDecoration: userChoice ? "underline" : "none"}}>{name}</span>
        <span>&emsp;&emsp; {userChoice === actualChoice ? "✔" : "✖"}</span>
    </>
  )
}
