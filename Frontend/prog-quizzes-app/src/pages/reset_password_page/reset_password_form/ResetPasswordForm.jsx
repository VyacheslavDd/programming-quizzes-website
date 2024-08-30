
import React, { useContext, useEffect, useState } from 'react'
import Form from '../../../components/form/Form';
import PasswordField from '../../../components/form/password_field/PasswordField';
import RepeatPasswordField from '../../../components/form/repeat_password_field/RepeatPasswordField';
import FormSubmit from '../../../components/form/form_submit/FormSubmit';
import FormErrorMessage from '../../../components/form/error_message/FormErrorMessage';
import useFormSubmit from '../../../hooks/useFormSubmit';
import UserAPI from '../../../services/API/UserAPI';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../../context/AuthContext';

export default function ResetPasswordForm({requestData}) {

    const { setToken } = useContext(AuthContext);

    const router = useNavigate();
    const [resetData, setResetData] = useState({password: "", repeatPassword: ""});
    const [corrects, setCorrects] = useState({isPasswordCorrect: false, isPasswordCorresponding: false});
    const [submitValue, isPending, isSuccess, message, submit] = useFormSubmit("Установить", "Ожидание...", async () => {
        return await UserAPI.resetPassword(requestData.userId, requestData.sequence, resetData.password, setToken);
    });

    const setResetInfo = (propertyName, value) => {
        setResetData(prev => ({...prev, [propertyName]: value}));
    }

    const onPasswordSet = async () => {
        await submit();
    }

    useEffect(() => {
        if (isSuccess) {
            router("/login", {state: {msg: "Пароль успешно обновлён"}});
        }
    }, [isSuccess])

  return (
    <>
        <Form title="Сброс пароля" onSubmit={() => onPasswordSet()}>
            <PasswordField input={resetData.password} setInput={setResetInfo} setIsCorrect={setCorrects} propertyName="password" correctPropertyName="isPasswordCorrect"/>
            <RepeatPasswordField input={resetData.repeatPassword} setInput={setResetInfo} setIsCorrect={setCorrects} propertyName="repeatPassword"
            originalPassword={resetData.password} correctPropertyName="isPasswordCorresponding"/>
            <FormSubmit isActive={corrects.isPasswordCorrect && corrects.isPasswordCorresponding && !isPending} value={submitValue}/>
        </Form>
        <FormErrorMessage message={message}/>
    </>
  )
}
