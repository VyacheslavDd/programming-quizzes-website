import { createRef, useEffect, useRef } from "react"
import GenericButton from "../../../components/UI/buttons/generic_button/GenericButton"
import GenericSelectInput from "../../../components/UI/select_input_ui/GenericSelectInput"
import UserAPI from "../../../services/API/UserAPI"
import styles from "./CabinetNotifications.module.css"
import React, { useState } from 'react'
import useFetching from "../../../hooks/useFetching"
import NotificationsAPI from "../../../services/API/NotificationsAPI"
import Loading from "../../../components/animations/Loading/Loading"
import ErrorMessage from "../../../components/error_message/ErrorMessage"
import Notification from "./notification/Notification"
import useObserver from "../../../hooks/useObserver"

export default function CabinetNotifications({user, setUser}) {

    const lastNotificationInSight = async (entries) => {
        if (entries[0].isIntersecting && page <= pagesCount) {
            let result = await NotificationsAPI.getUserNotifications(user, page);
            setPage(page + 1);
            setNotifications(prev => ([...prev, ...result.data.notifications]));
        }
    }

    const lastNotification = createRef();
    const lastNotificationObserver = useObserver(lastNotificationInSight, lastNotification);
    const [notifications, setNotifications] = useState(null);
    const [page, setPage] = useState(1);
    const [pagesCount, setPagesCount] = useState(0);
    const [fetchNotifications, isLoading, isError] = useFetching(async () => {
        let result = await NotificationsAPI.getUserNotifications(user, page);
        setPagesCount(Math.ceil(result.headers['content-count'] / 10));
        setPage(page + 1);
        setNotifications(result.data.notifications);
    }, true);

    const updateReceiveNotificationsOption = async (e) => {
        let result = await UserAPI.updateReceiveNotificationsOption(user, !user.receiveNotifications);
        if (result.responseCode === 200) {
            setUser(prev => ({...prev, receiveNotifications: !user.receiveNotifications}));
        }
    }

    const removeAllNotifications = async () => {
        await NotificationsAPI.removeAllNotifications(user);
        setNotifications({});
    }

    const removeNotification = async (notificationId) => {
        await NotificationsAPI.removeNotification(user, notificationId);
        setNotifications(notifications.filter((notification) => notification.id !== notificationId));
    }

    useEffect(() => {
        fetchNotifications();
    }, [])

  return (
    <>
        <div className={styles.notificationsOption}>
            <GenericSelectInput checked={user.receiveNotifications} onChange={(e) => updateReceiveNotificationsOption(e)} name="notifications" type="checkbox"
                value="notifications"/>
            <span className={styles.optionDescription}>Хочу получать уведомления о новых викторинах и прочих важных новостях</span>
        </div>
        <div className={styles.managementPanel}>
            <GenericButton onClick={removeAllNotifications}>Удалить все</GenericButton>
        </div>
        <div className={styles.notifications}>
            {isLoading
            ? <Loading/>
            : isError
            ? <ErrorMessage errorMsg="Не удалось загрузить уведомления"/>
            : notifications.length > 0
            ? notifications.map((notification, index) =>
                { if (index + 1 === notifications.length) {
                    return <Notification key={notification.id} content={notification.content} timestamp={notification.date} ref={lastNotification}
                    removeNotification={() => removeNotification(notification.id)}/>
                }
                return <Notification key={notification.id} content={notification.content} timestamp={notification.date}
              removeNotification={() => removeNotification(notification.id)}/>
            })
            : <span className={styles.noNotificationsSpan}>Пока уведомления отсутствуют</span>}
        </div>
    </>
  )
}
