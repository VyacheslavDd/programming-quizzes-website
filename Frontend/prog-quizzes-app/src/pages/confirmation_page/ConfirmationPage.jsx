import React, { useEffect, useRef, useState } from 'react'
import styles from "./ConfirmationPage.module.css"
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import ConfirmationsAPI from '../../services/API/ConfirmationsAPI';

export default function ConfirmationPage() {

    const [countDown, setCountDown] = useState(5);
    const [response, setResponse] = useState("");
    const router = useNavigate();
    const sequence = useParams()['sequence'];
    const countDownIntervalRef = useRef(null);

    useEffect(() => {
        async function confirmUser() {
            let response = await ConfirmationsAPI.confirm(sequence);
            if (response.responseCode !== 200) {
                setResponse(response.errorMessage);
            }
            else {
                setResponse("Аккаунт подтвержден!");
            }
            if (countDownIntervalRef.current === null) {
            countDownIntervalRef.current = setInterval(() => {
                setCountDown(prev => prev - 1);
            }, 1000)
        }
        }
        confirmUser();
    }, [])

    useEffect(() => {
        if (countDown <= 0) {
            clearInterval(countDownIntervalRef.current);
            router("/login");
        }
    }, [countDown])

  return (
    <div className={styles.confirmWindow}>
        <p className={styles.response}>{response}</p>
        <span className={styles.countDown}>Перенаправление на страницу авторизации через {countDown}...</span>
    </div>
  )
}
