import React, { useContext, useEffect, useState } from 'react'
import styles from "./LoginPage.module.css"
import Form from '../../components/form/Form'
import Helper from '../../services/Helper'
import FormField from '../../components/form/form_field/FormField'
import EmailField from '../../components/form/email_field/EmailField'
import PasswordField from '../../components/form/password_field/PasswordField'
import LoginField from '../../components/form/login_field/LoginField'
import LoginEmailField from '../../components/form/email_or_login_field/LoginEmailField'
import RepeatPasswordField from '../../components/form/repeat_password_field/RepeatPasswordField'
import FormSubmit from '../../components/form/form_submit/FormSubmit'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { AuthContext } from '../../context/AuthContext'
import useFormSubmit from '../../hooks/useFormSubmit'
import UserAPI from '../../services/API/UserAPI'
import FormErrorMessage from '../../components/form/error_message/FormErrorMessage'
import SuccessContainer from '../../components/success_container/SuccessContainer'
import TokenHelper from '../../services/TokenHelper'

export default function LoginPage() {

  const [loginData, setLoginData] = useState({loginMail: "", password: ""});
  const [corrects, setCorrects] = useState({isLoginMailCorrect: false, isPasswordCorrect: false});
  const [showSuccess, setShowSuccess] = useState(true);
  const [loginResult, setLoginResult] = useState(null);
  const [submitValue, isPending, isSuccess, message, doSubmit] = useFormSubmit("Войти", "Выполняется вход...", async () => {
    return await UserAPI.authenticate(loginData.loginMail, loginData.password);
  }) 

  const router = useNavigate();
  const { state } = useLocation();
  const { setToken } = useContext(AuthContext);

  const authenticate = async () => {
    let result = await doSubmit();
    setLoginResult(prev => result);
  }

  const setLoginInfo = (propertyName, value) => {
    setLoginData(prev => ({...prev, [propertyName]: value}))
  }

  useEffect(() => {
    if (isSuccess) {
      localStorage.setItem(TokenHelper.tokenStorageKey, loginResult.token);
      setToken(loginResult.token);
      router("/");
    }
  }, [isSuccess])

  useEffect(() => {
    let timeout;
    if (state !== null) {
      timeout = setTimeout(() => {
        setShowSuccess(false);
      }, 5000)
    }

    return () => clearTimeout(timeout);
  }, [state])

  return (
    <div className={styles.mainContainer}>
        {state && showSuccess &&  <SuccessContainer message={state.msg}/>}
        <Form title="Авторизация" onSubmit={() => authenticate()}>
          <LoginEmailField propertyName="loginMail" correctPropertyName="isLoginMailCorrect" input={loginData.loginMail} setInput={setLoginInfo} setIsCorrect={setCorrects}/>
          <PasswordField propertyName="password" correctPropertyName="isPasswordCorrect" input={loginData.password} setInput={setLoginInfo} setIsCorrect={setCorrects}/>
          <div className={styles.btnContainer}>
            <Link to="/send-reset" className={styles.forgotPasswordLink}>Забыли пароль?</Link>
            <FormSubmit isActive={corrects.isLoginMailCorrect && corrects.isPasswordCorrect && !isPending} value={submitValue}/>
          </div>
        </Form>
        <FormErrorMessage message={message}/>
    </div>
  )
}
