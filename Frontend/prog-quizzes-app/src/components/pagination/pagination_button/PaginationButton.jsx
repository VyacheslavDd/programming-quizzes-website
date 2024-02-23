import styles from "./PaginationButton.module.css"
import React from 'react'

export default function PaginationButton({children, isCurrent, ...props}) {
  return (
    <button {...props} className={`${styles.button} ${isCurrent ? styles.current: styles.pagButton}`}>{children}</button>
  )
}
