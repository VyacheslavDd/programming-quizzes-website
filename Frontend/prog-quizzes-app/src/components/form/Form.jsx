import React from 'react'
import styles from "./Form.module.css"

export default function Form({title, onSubmit, children}) {

    const submitForm = (e) => {
        e.preventDefault();
        onSubmit();
    }

  return (
    <div className={styles.formContainer}>
        <h2 className={styles.formTitle}>{title}</h2>
        <form action='post' className={styles.form} onSubmit={(e) => submitForm(e)}>
            {children}
        </form>
    </div>
  )
}
