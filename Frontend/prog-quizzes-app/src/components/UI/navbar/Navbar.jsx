
import { Link } from "react-router-dom"
import styles from "./Navbar.module.css"
import { useContext } from "react"
import { AuthContext } from "../../../contexts/AuthContext"

function Navbar() {

    const {isAuth, setIsAuth} = useContext(AuthContext);

    return(
        <>
            <nav className={styles.navigation}>
                <ul className={styles.navigationList}>
                    <li className={styles.navigationItem}><Link to="/posts">Посты</Link></li>
                    <li className={styles.navigationItem}><Link to="/about">О сайте</Link></li>
                    {!isAuth
                    ?
                        <li className={styles.navigationItem}><Link to="/login">Авторизоваться</Link></li>
                    :
                        <li className={styles.navigationItem}><Link to="/" onClick={() => {setIsAuth(false); localStorage.removeItem('isAuth')}}>Выйти</Link></li>
                    }
                </ul>
            </nav>
        </>
    )
}

export default Navbar