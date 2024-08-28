
import React, { useEffect, useRef, useState } from 'react'
import styles from "./ResetPasswordPage.module.css"
import { useNavigate, useParams } from 'react-router-dom'
import PasswordRequestsAPI from '../../services/API/PasswordRequestsAPI';
import ResetPasswordForm from './reset_password_form/ResetPasswordForm';
import ResetPasswordError from './reset_password_error/ResetPasswordError';

export default function ResetPasswordPage() {

    const sequence = useParams()['sequence'];
    const [isCorrectLink, setIsCorrectLink] = useState(true);
    const [errorMessage, setErrorMessage] = useState("");
    const [requestData, setRequestData] = useState(null);

    useEffect(() => {
        async function getRequest() {
            let response = await PasswordRequestsAPI.getRequest(sequence);
            if (response.responseCode !== 200) {
                setIsCorrectLink(false);
                setErrorMessage(response.errorMessage);
            }
            else {
                setRequestData(response.requestData);
            }
        }
        getRequest();
    }, [])

  return (
    <div className={styles.mainContainer}>
    {isCorrectLink && <ResetPasswordForm requestData={requestData}/>}
    {!isCorrectLink && <ResetPasswordError errorMessage={errorMessage}/>}
    </div>
  )
}
