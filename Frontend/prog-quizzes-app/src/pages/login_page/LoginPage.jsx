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
import { useNavigate } from 'react-router-dom'
import { AuthContext } from '../../context/AuthContext'

export default function LoginPage() {

  const [loginMail, setLoginMail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoginMailCorrect, setIsLoginMailCorrect] = useState(false);
  const [isPasswordCorrect, setIsPasswordCorrect] = useState(false);

  const router = useNavigate();
  const { token, setToken } = useContext(AuthContext);

  return (
    <div className={styles.mainContainer}>
        <Form title="Авторизация" onSubmit={() => setToken("md")}>
          <LoginEmailField input={loginMail} setInput={setLoginMail} setIsCorrect={setIsLoginMailCorrect}/>
          <PasswordField input={password} setInput={setPassword} setIsCorrect={setIsPasswordCorrect}/>
          <FormSubmit isActive={isLoginMailCorrect && isPasswordCorrect} value="Войти"/>
        </Form>
    </div>
  )
}
