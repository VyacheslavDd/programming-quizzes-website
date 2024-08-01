import React from 'react'
import styles from "./MainPage.module.css"

export default function MainPage() {
  return (
    <div className={styles.outer}>
      <div className={styles.wrapper}>
        <section className={styles.section}>
          <h2>Добро пожаловать! Это веб-сайт Quizz</h2>
          <p>Данный сервис будет содержать викторины по самым разнообразным темам для таких языков программирования,
            как <strong>Python</strong>,  <strong>C#</strong>,  <strong>JavaScript</strong> и прочих.
          </p>
          <p>Здесь ты можешь проверить свои знания и хорошо провести время!</p>
        </section>
        <section className={styles.section}>
          <h2>Какие викторины здесь будут содержаться?</h2>
          <p>Викторины от 10 до 50 вопросов, с заданиями на одиночный или множественный выбор ответа.</p>
          <p>Также викторины подразделяются по уровням сложности:</p>
          <ul className={styles.difList}>
            <li><span className={styles.easyDif}>Легкие</span> - для тех, кто только начинает познавать основы определённой технологии;</li>
            <li><span className={styles.medDif}>Средние</span> - для тех, кто разбирается в выбранной теме на хорошем уровне;</li>
            <li><span className={styles.hardDif}>Трудные</span> - для тех, кто очень глубоко понимает тему;</li>
            <li><span className={styles.expDif}>Эксперт</span> - настоящее испытание для самых-самых молодцов!</li>
          </ul>
        </section>
        
      </div>
    </div>
  )
}
