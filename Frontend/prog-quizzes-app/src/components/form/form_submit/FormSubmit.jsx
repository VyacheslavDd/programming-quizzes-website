import React from 'react'
import styles from "./FormSubmit.module.css"

export default function FormSubmit({isActive, value}) {
  return (
    <input disabled={!isActive} className={styles.submitInput} type='submit' value={value}/>
  )
}
