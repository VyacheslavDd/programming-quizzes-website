import React from 'react'
import styles from "./FormField.module.css"

export default function FormField({type, placeholder, label, setInput, hint}) {
  return (
    <div className={styles.fieldContainer}>
        <label className={styles.label}>{label}</label>
        <input onChange={(e) => setInput(e.target.value)} className={styles.formField} type={type} placeholder={placeholder}/>
        <div className={styles.hintContainer}>
        <span className={styles.inputHint}>{hint}</span>
        </div>
    </div>
  )
}
