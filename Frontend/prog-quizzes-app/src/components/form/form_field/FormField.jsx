import React from 'react'
import styles from "./FormField.module.css"

export default function FormField({type, placeholder, label, propertyName, setInput, hint, defaultValue="", ...props}) {
  return (
    <div className={styles.fieldContainer}>
        <label className={styles.label}>{label}</label>
        <input defaultValue={defaultValue} onChange={(e) => setInput(propertyName, e.target.value)} className={styles.formField}
        type={type} placeholder={placeholder} {...props}/>
        <div className={styles.hintContainer}>
        <span className={styles.inputHint}>{hint}</span>
        </div>
    </div>
  )
}
