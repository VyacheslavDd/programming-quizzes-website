import styles from "./MyInput.module.css"
import React from 'react'

export default function MyInput(props) {
  return (
    <input className={styles.myInput} {...props}></input>
  )
}
