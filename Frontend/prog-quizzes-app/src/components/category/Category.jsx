import React from 'react'
import styles from "./Category.module.css"

export default function Category({name}) {
  return (
    <span className={styles.categorySpan}><strong>{name}</strong></span>
  )
}
