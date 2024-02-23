import Category from "../category/Category"
import styles from "./CategoryDifficulty.module.css"
import React from 'react'

export default function CategoryDifficulty({categoryName, difficulty}) {
  return (
    <div className={styles.categoryDif}>
        <Category name={categoryName}/>
        <span className={styles.difSpan}>{difficulty}</span>
    </div>
  )
}
