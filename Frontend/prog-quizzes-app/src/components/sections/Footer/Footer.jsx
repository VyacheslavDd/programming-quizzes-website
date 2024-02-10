import React from 'react'
import styles from "./Footer.module.css"

export default function Footer() {
  return (
    <footer className={styles.footerBlock}>
        <span className={styles.copyright}>Quizz, ©️ 2023</span>
    </footer>
  )
}
