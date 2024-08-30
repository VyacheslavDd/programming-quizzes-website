import React, { useContext, useEffect, useMemo } from 'react'
import styles from "./Header.module.css"
import logo from "../../../assets/quizz-logo.png"
import { useNavigate } from 'react-router-dom'
import { Link } from 'react-router-dom'
import { AuthContext } from '../../../context/AuthContext'
import Helper from '../../../services/Helper'
import TokenHelper from '../../../services/TokenHelper'

export default function Header() {

  const { token } = useContext(AuthContext);

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
                {TokenHelper.isTokenInvalid(token) || TokenHelper.isTokenExpired(token)
                ?
                <>
                  <li className={styles.logLinksItem}><Link to='/login' className={styles.logInButton}>Войти</Link></li>
                  <li className={styles.logLinksItem}><Link to='/register' className={styles.regButton}>Регистрация</Link></li>
                </>
                :
                <>
                  <li className={styles.logLinksItem}><Link to='/cabinet' className={styles.cabinetButton}>Личный кабинет</Link></li>
                </>
                }
            </ul>
          </div>
      </header>
    </>
  )
}
