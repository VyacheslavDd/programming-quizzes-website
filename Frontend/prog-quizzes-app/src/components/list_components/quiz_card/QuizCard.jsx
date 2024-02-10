import React from 'react'
import styles from "./QuizCard.module.css"
import Category from '../../category/Category'
import Subcategory from '../../subcategory/Subcategory'

export default function QuizCard() {
  return (
    <div className={styles.cardBlock}>
        <div className={styles.container}>
            <img className={styles.quizImage} src='https://placehold.co/230x200' alt='Изображение викторины'></img>
            <h2>Название викторины небольшое</h2>
            <p className={styles.description}>Здесь находится небольшое описание викторины</p>
            <div className={styles.categoryDif}>
                <Category name="C#"/>
                <span className={styles.difSpan}>★★★ Сложный</span>
            </div>
            <div className={styles.subcategories}>
                <Subcategory name="ASP.Net Core"/>
                <Subcategory name="Func"/>
            </div>
            <span className={styles.dateSpan}>2023-11-02 23:30</span>
        </div>
    </div>
  )
}
