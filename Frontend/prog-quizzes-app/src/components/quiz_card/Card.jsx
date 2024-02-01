import styles from "./Card.module.css"
import React from 'react'

export default function () {
  return (
    <div className={styles.cardBlock}>
        <img className={styles.cardImage} src='https://placehold.co/600x400' alt='Картинка карточки'></img>
        <p className={styles.titleCard}><strong>Делегаты и функции</strong></p>
        <div className={styles.difficultyBlock}>
            <span className={styles.difficultySpan}>Легкий</span>
        </div>
        <div className={styles.categoriesBlock}>
            <span className={styles.categorySpan}>Категория 1</span>
            <span className={styles.categorySpan}>Категория 1</span>
            <span className={styles.categorySpan}>Категория 1</span>
        </div>
        <span className={styles.dateSpan}>13-09-2003</span>
    </div>
  )
}
