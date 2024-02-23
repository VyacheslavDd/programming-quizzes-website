import styles from "./GenericButton.module.css"
import React from 'react'

export default function GenericButton({children, ...props}) {
  return (
    <button {...props} className={styles.button}>{children}</button>
  )
}
