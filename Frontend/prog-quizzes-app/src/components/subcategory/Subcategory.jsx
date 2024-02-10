import React from 'react'
import styles from "./Subcategory.module.css"

export default function Subcategory({name}) {
  return (
    <span className={styles.subcategorySpan}>
        <i>{name}</i>
    </span>
  )
}
