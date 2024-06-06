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

    const [login, setLogin] = useState("");
    const [isLoginCorrect, setIsLoginCorrect] = useState(false);
    const [email, setEmail] = useState("");
    const [isEmailCorrect, setIsEmailCorrect] = useState(false);
    const [password, setPassword] = useState("");
    const [isPasswordCorrect, setIsPasswordCorrect] = useState(false);
    const [repeatPassword, setRepeatPassword] = useState("");
    const [isPasswordCorresponding, setIsPasswordCorresponding] = useState(false);
    const [submitValue, isPending, isSuccess, message, doSubmit] = useFormSubmit("Зарегистрироваться", "Регистрируем...", async () => {
      return await UserAPI.register(email, login, password);
    }) 
    
    const router = useNavigate();

    const canSubmit = () => {
        return isLoginCorrect && isEmailCorrect && isPasswordCorrect && isPasswordCorresponding && !isPending;
    }

    const register = async () => {
      let result = await doSubmit();
    }

    useEffect(() => {
      if (isSuccess) {
        router("/login", { state: {msg: "Аккаунт успешно зарегистрирован!" }});
      }
    }, [isSuccess])

  return (
    <div className={styles.mainContainer}>
        <Form title="Регистрация" onSubmit={() => register()}>
            <EmailField input={email} setInput={setEmail} setIsCorrect={setIsEmailCorrect}/>
            <LoginField input={login} setInput={setLogin} setIsCorrect={setIsLoginCorrect}/>
            <PasswordField input={password} setInput={setPassword} setIsCorrect={setIsPasswordCorrect}/>
            <RepeatPasswordField input={repeatPassword} setInput={setRepeatPassword} setIsCorrect={setIsPasswordCorresponding}
            originalPassword={password}/>
            <FormSubmit value={submitValue} isActive={canSubmit()}/>
        </Form>
        <FormErrorMessage message={message}/>
    </div>
  )
}
