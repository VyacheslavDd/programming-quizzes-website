
import React from 'react'
import styles from "./ResetPasswordError.module.css"
import { Link, useNavigate } from 'react-router-dom'

export default function ResetPasswordError({errorMessage}) {

  return (
    <>
        <div className={styles.errorBlock}>Упс! {errorMessage}.</div>
        <Link to="/login" className={styles.link}>К авторизации {`>>>`}</Link>
    </>
  )
}
