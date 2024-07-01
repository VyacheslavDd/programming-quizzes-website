import React from 'react'
import styles from "./AsideButton.module.css"

export default function AsideButton({title, isActive, value, setValue}) {
  return (
    <div className={isActive ? styles.active : styles.inactive} onClick={() => setValue(value)}>{title}</div>
  )
}
