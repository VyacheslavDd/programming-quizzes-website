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
import { useLocation, useNavigate } from 'react-router-dom'
import { AuthContext } from '../../context/AuthContext'
import useFormSubmit from '../../hooks/useFormSubmit'
import UserAPI from '../../services/API/UserAPI'
import FormErrorMessage from '../../components/form/error_message/FormErrorMessage'
import SuccessContainer from '../../components/success_container/SuccessContainer'

export default function LoginPage() {

  const [loginMail, setLoginMail] = useState("");
  const [showSuccess, setShowSuccess] = useState(true);
  const [password, setPassword] = useState("");
  const [isLoginMailCorrect, setIsLoginMailCorrect] = useState(false);
  const [isPasswordCorrect, setIsPasswordCorrect] = useState(false);
  const [loginResult, setLoginResult] = useState(null);
  const [submitValue, isPending, isSuccess, message, doSubmit] = useFormSubmit("Войти", "Выполняется вход...", async () => {
    return await UserAPI.authenticate(loginMail, password);
  }) 

  const router = useNavigate();
  const { state } = useLocation();
  const { setToken } = useContext(AuthContext);

  const authenticate = async () => {
    let result = await doSubmit();
    setLoginResult(result);
  }

  useEffect(() => {
    if (isSuccess) {
      localStorage.setItem(Helper.tokenStorageKey, loginResult.token);
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
          <LoginEmailField input={loginMail} setInput={setLoginMail} setIsCorrect={setIsLoginMailCorrect}/>
          <PasswordField input={password} setInput={setPassword} setIsCorrect={setIsPasswordCorrect}/>
          <FormSubmit isActive={isLoginMailCorrect && isPasswordCorrect && !isPending} value={submitValue}/>
        </Form>
        <FormErrorMessage message={message}/>
    </div>
  )
}
