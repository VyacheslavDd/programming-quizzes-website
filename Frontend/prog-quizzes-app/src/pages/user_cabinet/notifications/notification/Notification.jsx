import Helper from "../../../../services/Helper"
import styles from "./Notification.module.css"
import React, { forwardRef, useRef } from 'react'

export default forwardRef(function Notification({content, timestamp, removeNotification, isNew}, ref) {

    const fixedTimestamp = useRef(timestamp.match(Helper.dateRegex)[0]);

    return (
        <div className={`${styles.notificationCard} ${isNew && styles.newNotificationCard}`} ref={ref}>
            <p className={styles.content}>{content}</p>
            <span className={styles.timestamp}>{fixedTimestamp.current}</span>
            <button className={styles.deleteNotificationBtn} onClick={removeNotification}>X</button>
        </div>
  )
})
