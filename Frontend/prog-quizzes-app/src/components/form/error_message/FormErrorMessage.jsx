
import React, { useEffect } from 'react'
import styles from "./FormErrorMessage.module.css"

export default function FormErrorMessage({message}) {

  return (
    <span className={styles.errorSpan}>{message}</span>
  )
}
