import AsideButton from "../AsideButton"
import styles from "./NotificationsAsideButton.module.css"
import React from 'react'

export default function NotificationsAsideButton({title, isActive, value, setValue, newNotificationsCount}) {
  return (
    <div className={styles.notificationsAsideContainer}>
        <AsideButton title={title} value={value} isActive={isActive} setValue={setValue}/>
        {newNotificationsCount > 0 && <span className={styles.notificationsCount}>{newNotificationsCount}</span>}
    </div>
  )
}
