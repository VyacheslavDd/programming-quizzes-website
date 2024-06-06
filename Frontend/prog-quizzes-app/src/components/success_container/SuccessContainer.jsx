import React from 'react'
import styles from "./SuccessContainer.module.css"

export default function SuccessContainer({message}) {
  return (
    <div className={styles.container}>{message}</div>
  )
}
