
import React from 'react'
import styles from "./ErrorMessage.module.css"

export default function ErrorMessage({errorMsg}) {
  return (
    <h1 className={styles.errorMsg}>{errorMsg}</h1>
  )
}
