import { forwardRef } from "react"
import styles from "./OptionsSelect.module.css"
import React from 'react'

export default forwardRef(function OptionsSelect({label, options, onChange}, ref) {
  return (
    <div className={styles.selectionDiv}>
        <label className={styles.selectLabel}>{label}</label>
        <select ref={ref} className={styles.select} defaultValue={options[0]?.value} onChange={(e) => onChange(e)}>
            {options.map(option => <option className={styles.option} key={option.id} value={option.value}>{option.text}</option>)}
        </select>
    </div>
  )
})
