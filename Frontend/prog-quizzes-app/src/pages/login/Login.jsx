
import React, { useContext } from 'react'
import MyInput from '../../components/UI/input/MyInput'
import MyButton from '../../components/UI/button/MyButton'
import { AuthContext } from '../../contexts/AuthContext'

export default function () {

    const setIsAuth = useContext(AuthContext)['setIsAuth']

    const doLogin = () => {
        localStorage.setItem("isAuth", 'true');
        setIsAuth(true);
    }

    return (
    <div>
        <h1>Страница авторизации</h1>
        <MyInput type="email" placeholder="Почта..."></MyInput>
        <MyInput type="password" placeholder="Пароль..."></MyInput>
        <MyButton onClick={doLogin}>Войти</MyButton>
    </div>
  )
}
