import React, { useContext } from 'react'
import styles from "./CabinetAside.module.css"
import AsideButton from '../../../components/aside_button/AsideButton'
import GenericButton from '../../../components/UI/buttons/generic_button/GenericButton'
import { AuthContext } from '../../../context/AuthContext'
import Helper from '../../../services/Helper'

export default function CabinetAside({currentComponent, setComponent}) {

  const { token, setToken } = useContext(AuthContext);

  const logout = () => {
    setToken("");
    localStorage.removeItem(Helper.tokenStorageKey);
  }

  return (
    <aside className={styles.asideBlock}>
        <AsideButton title="Основная информация" isActive={currentComponent === "main"} value="main" setValue={setComponent}/>
        <AsideButton title="Смена пароля" value="password" isActive={currentComponent === "password"} setValue={setComponent}/>
        <AsideButton title="Уведомления" value="notifications" isActive={currentComponent === "notifications"} setValue={setComponent}/>
        <div className={styles.exit}>
          <GenericButton onClick={logout}>Выйти из аккаунта</GenericButton>
        </div>
    </aside>
  )
}
