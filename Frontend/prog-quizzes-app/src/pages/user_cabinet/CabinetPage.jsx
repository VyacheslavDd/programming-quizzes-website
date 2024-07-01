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

export default function CabinetPage() {
    const [user, setUser] = useState(null);
    const [currentComponent, setCurrentComponent] = useState("main");
    const [fetchUser, isLoading, isError] = useFetching(async () => {
        const token = localStorage.getItem(Helper.tokenStorageKey);
        const guid = jwtDecode(token)['Id'];
        const user = await UserAPI.getUserData(guid);
        setUser(user.data);
    }, true)

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
             <CabinetAside currentComponent={currentComponent} setComponent={setCurrentComponent}/>
             <CabinetMain>
                {currentComponent === "main" && <CabinetMainInfo user={user} setUser={setUser}/>}
                {currentComponent === "password" && <CabinetPasswordControl user={user}/>}
             </CabinetMain>
        </div>}
    </>
  )
}
