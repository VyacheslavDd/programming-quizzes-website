import React, { useEffect, useState } from 'react'
import styles from "./RegistrationPage.module.css"
import Form from '../../components/form/Form'
import LoginEmailField from '../../components/form/email_or_login_field/LoginEmailField'
import EmailField from '../../components/form/email_field/EmailField'
import FormSubmit from '../../components/form/form_submit/FormSubmit'
import PasswordField from '../../components/form/password_field/PasswordField'
import RepeatPasswordField from '../../components/form/repeat_password_field/RepeatPasswordField'
import LoginField from '../../components/form/login_field/LoginField'
import { useNavigate } from 'react-router-dom'
import useFormSubmit from '../../hooks/useFormSubmit'
import UserAPI from '../../services/API/UserAPI'
import FormErrorMessage from '../../components/form/error_message/FormErrorMessage'

export default function RegistrationPage() {
    const [registerData, setRegisterData] = useState({login: "", email: "", password: "", repeatPassword: ""});
    const [corrects, setCorrects] = useState({isLoginCorrect: false, isEmailCorrect: false, isPasswordCorrect: false, isPasswordCorresponding: false});
    const [submitValue, isPending, isSuccess, message, doSubmit] = useFormSubmit("Зарегистрироваться", "Регистрируем...", async () => {
      return await UserAPI.register(registerData.email, registerData.login, registerData.password);
    }) 
    
    const router = useNavigate();

    const canSubmit = () => {
        for (let value of Object.values(corrects)) {
          if (!value) return false;
        }
        return !isPending;
    }

    const setRegisterInfo = (propertyName, value) => {
      setRegisterData(prev => ({...prev, [propertyName]: value}))
    }

    const register = async () => {
      let result = await doSubmit();
    }

    useEffect(() => {
      if (isSuccess) {
        router("/login", { state: {msg: "Регистрация почти завершена! Проверьте почту для завершения." }});
      }
    }, [isSuccess])

  return (
    <div className={styles.mainContainer}>
        <Form title="Регистрация" onSubmit={() => register()}>
            <EmailField propertyName="email" correctPropertyName="isEmailCorrect" input={registerData.email} setInput={setRegisterInfo} setIsCorrect={setCorrects}/>
            <LoginField propertyName="login" correctPropertyName="isLoginCorrect" input={registerData.login} setInput={setRegisterInfo} setIsCorrect={setCorrects}/>
            <PasswordField propertyName="password" correctPropertyName="isPasswordCorrect" input={registerData.password} setInput={setRegisterInfo}
            setIsCorrect={setCorrects}/>
            <RepeatPasswordField propertyName="repeatPassword" correctPropertyName="isPasswordCorresponding" input={registerData.repeatPassword}
            setInput={setRegisterInfo} setIsCorrect={setCorrects}
            originalPassword={registerData.password}/>
            <FormSubmit value={submitValue} isActive={canSubmit()}/>
        </Form>
        <FormErrorMessage message={message}/>
    </div>
  )
}
