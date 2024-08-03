import React, { useEffect, useRef } from 'react'
import styles from "./CabinetPage.module.css"
import CabinetAside from './aside/CabinetAside'
import useFetching from '../../hooks/useFetching'
import UserAPI from '../../services/API/UserAPI'
import Helper from '../../services/Helper'
import { jwtDecode } from 'jwt-decode'
import { useState } from 'react'
import Loading from '../../components/animations/Loading/Loading'
import ErrorMessage from '../../components/error_message/ErrorMessage'
import CabinetMain from './main/CabinetMain'
import GenericButton from '../../components/UI/buttons/generic_button/GenericButton'
import CabinetMainInfo from './main_data/CabinetMainInfo'
import CabinetPasswordControl from './password/CabinetPasswordControl'
import CabinetNotifications from './notifications/CabinetNotifications'

export default function CabinetPage() {
    const [user, setUser] = useState(null);
    const [currentComponent, setCurrentComponent] = useState("main");
    const [fetchUser, isLoading, isError] = useFetching(async () => {
        const token = localStorage.getItem(Helper.tokenStorageKey);
        const guid = jwtDecode(token)['Id'];
        const user = await UserAPI.getUserData(guid);
        setUser(user.data);
    }, true)

    const updateUserInfo = (propertyName, value) => {
        setUser(prev => ({...prev, userInfo: ({...prev.userInfo, [propertyName]: value})}));
    }

    useEffect(() => {
        async function getUser() {
            await fetchUser();
        }
        let abortController = new AbortController();
        getUser();
        return () => {
            abortController.abort();
        }
    }, [])

  return (
    <>
        {isLoading
        ? <Loading/>
        : isError
        ? <ErrorMessage errorMsg="Не удалось получить данные. Попробуйте позже"/>
        : <div className={styles.main}>
             <CabinetAside user={user} currentComponent={currentComponent} setComponent={setCurrentComponent}/>
             <CabinetMain>
                {currentComponent === "main" && <CabinetMainInfo user={user.userInfo} setUser={updateUserInfo} userId={user.id}/>}
                {currentComponent === "password" && <CabinetPasswordControl user={user}/>}
                {currentComponent === "notifications" && <CabinetNotifications user={user.userNotificationsInfo} setUser={setUser} userId={user.id}/>}
             </CabinetMain>
        </div>}
    </>
  )
}
