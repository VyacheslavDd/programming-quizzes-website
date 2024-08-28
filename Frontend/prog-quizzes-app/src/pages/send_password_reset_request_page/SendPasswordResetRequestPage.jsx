
import React, { useEffect, useState } from 'react'
import styles from "./SendPasswordResetRequestPage.module.css"
import Form from '../../components/form/Form'
import EmailField from '../../components/form/email_field/EmailField';
import FormSubmit from '../../components/form/form_submit/FormSubmit';
import useFormSubmit from '../../hooks/useFormSubmit';
import FormErrorMessage from '../../components/form/error_message/FormErrorMessage';
import PasswordRequestsAPI from '../../services/API/PasswordRequestsAPI';
import { useNavigate } from 'react-router-dom';

export default function SendPasswordResetRequestPage() {

    const router = useNavigate();
    const [requestData, setRequestData] = useState({email: ""});
    const [corrects, setCorrects] = useState({isEmailCorrect: false});
    const [submitValue, isPending, isSuccess, message, submit] = useFormSubmit("Сбросить пароль", "Ожидание", async () => {
        return await PasswordRequestsAPI.sendRequest(requestData.email);
    });

    const setRequestInfo = (propertyName, value) => {
        setRequestData(prev => ({...prev, [propertyName]: value}))
      }

    const onRequest = async () => {
        await submit();
    }

    useEffect(() => {
        if (isSuccess) {
            router("/login", { state: {msg: "На вашу почту выслано письмо с дальнейшими инструкциями" }});
        }
    }, [isSuccess])

  return (
    <div className={styles.mainContainer}>
        <Form title="Запрос на сброс пароля" onSubmit={() => onRequest()}>
            <EmailField input={requestData.email} setInput={setRequestInfo} setIsCorrect={setCorrects} propertyName="email" correctPropertyName="isEmailCorrect"/>
            <FormSubmit isActive={corrects.isEmailCorrect && !isPending} value={submitValue}/>
        </Form>
        <FormErrorMessage message={message}/>
    </div>
  )
}
