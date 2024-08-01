import React, { useContext } from 'react'
import styles from "./CabinetAside.module.css"
import AsideButton from '../../../components/aside_button/AsideButton'
import GenericButton from '../../../components/UI/buttons/generic_button/GenericButton'
import { AuthContext } from '../../../context/AuthContext'
import Helper from '../../../services/Helper'
import NotificationsAsideButton from '../../../components/aside_button/notifications_aside_button/NotificationsAsideButton'

export default function CabinetAside({user, currentComponent, setComponent}) {

  const { token, setToken } = useContext(AuthContext);

  const logout = () => {
    setToken("");
    localStorage.removeItem(Helper.tokenStorageKey);
  }

  return (
    <aside className={styles.asideBlock}>
        <AsideButton title="Основная информация" isActive={currentComponent === "main"} value="main" setValue={setComponent}/>
        <AsideButton title="Смена пароля" value="password" isActive={currentComponent === "password"} setValue={setComponent}/>
        <NotificationsAsideButton title="Уведомления" value="notifications" isActive={currentComponent === "notifications"} setValue={setComponent}
        newNotificationsCount={user.newNotificationsCount}/>
        <div className={styles.exit}>
          <GenericButton onClick={logout}>Выйти из аккаунта</GenericButton>
        </div>
    </aside>
  )
}
