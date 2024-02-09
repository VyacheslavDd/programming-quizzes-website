import React, { useEffect } from 'react'
import styles from "./Header.module.css"
import logo from "../../../assets/quizz-logo.png"
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

export default function Header() {

  return (
    <>
      <div className={styles.header}>
          <a href='#'><img src={logo} alt='Логотип сайта' width={150} height={100}/></a>
          <nav className={styles.navBar}>
            <ul className={styles.navigationLinks}>
                <li className={styles.navigationLinksItem}><Link to='/'>Главная</Link></li>
                <li className={styles.navigationLinksItem}><Link to='/about'>О сайте</Link></li>
                <li className={styles.navigationLinksItem}><a href='#'>Категории</a></li>
            </ul>
          </nav>
          <div className={styles.logDiv}>
          <ul className={styles.logLinks}>
                <li className={styles.logLinksItem}><a href='#' className={styles.logInButton}>Войти</a></li>
                <li className={styles.logLinksItem}><a href='#' className={styles.regButton}>Регистрация</a></li>
            </ul>
          </div>
      </div>
    </>
  )
}
