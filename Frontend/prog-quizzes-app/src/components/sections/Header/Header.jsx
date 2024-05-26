import React, { useEffect } from 'react'
import styles from "./Header.module.css"
import logo from "../../../assets/quizz-logo.png"
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'

export default function Header() {

  return (
    <>
      <header className={styles.header}>
          <Link to='/'><img src={logo} alt='Логотип сайта' width={150} height={100}/></Link>
          <nav className={styles.navBar}>
            <ul className={styles.navigationLinks}>
                <li className={styles.navigationLinksItem}><Link to='/'>Главная</Link></li>
                <li className={styles.navigationLinksItem}><Link to='/quizzes'>Викторины</Link></li>
            </ul>
          </nav>
          <div className={styles.logDiv}>
          <ul className={styles.logLinks}>
                <li className={styles.logLinksItem}><Link to='/login' className={styles.logInButton}>Войти</Link></li>
                <li className={styles.logLinksItem}><Link to='/register' className={styles.regButton}>Регистрация</Link></li>
            </ul>
          </div>
      </header>
    </>
  )
}
