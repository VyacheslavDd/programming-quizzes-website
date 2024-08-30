import React, { useContext, useEffect, useRef, useState } from 'react'
import styles from "./CabinetPasswordControl.module.css"
import EmailField from '../../../components/form/email_field/EmailField'
import FormField from '../../../components/form/form_field/FormField';
import Helper from '../../../services/Helper';
import PasswordField from '../../../components/form/password_field/PasswordField';
import RepeatPasswordField from '../../../components/form/repeat_password_field/RepeatPasswordField';
import FormSubmit from '../../../components/form/form_submit/FormSubmit';
import useFormSubmit from '../../../hooks/useFormSubmit';
import UserAPI from '../../../services/API/UserAPI';
import FormErrorMessage from '../../../components/form/error_message/FormErrorMessage';
import useSuccessTimeout from '../../../hooks/useSuccessTimeout';
import SuccessContainer from '../../../components/success_container/SuccessContainer';
import { AuthContext } from '../../../context/AuthContext';

export default function CabinetPasswordControl({user}) {

  const { setToken } = useContext(AuthContext);

  const [passwordData, setPasswordData] = useState({oldPassword: "", newPassword: "", repeatPassword: ""});
  const [corrects, setCorrects] = useState({isPasswordCorrect: false, isRepeatCorrect: false});
  const [isShowingSuccess, setIsShowingSuccess, doTimeout] = useSuccessTimeout(3000);
  const [submitValue, isPending, isSuccess, message, submit] = useFormSubmit("Сменить пароль", "Смена пароля...", async () => {
    return await UserAPI.updatePassword(user.id, passwordData, setToken);
  });
  const updatePassword = async (e) => {
    e.preventDefault();
    await submit();
  }

  const setPasswordInfo = (propertyName, value) => {
    setPasswordData(prev => ({...prev, [propertyName]: value}))
  }

  useEffect(() => {
    let timeout;
    if (isSuccess) {
      timeout = doTimeout();
    }
    else {
      setIsShowingSuccess(false);
    }
    return () => clearTimeout(timeout);
  }, [isSuccess])

  return (
      <form className={styles.form} action='post' onSubmit={(e) => updatePassword(e)}>
        <FormField propertyName="oldPassword" type={Helper.inputPasswordType} label="Старый пароль" placeholder="Введите пароль..." setInput={setPasswordInfo}/>
        <PasswordField propertyName="newPassword" correctPropertyName="isPasswordCorrect" input={passwordData.newPassword} setInput={setPasswordInfo}
        setIsCorrect={setCorrects}/>
        <RepeatPasswordField propertyName="repeatPassword" correctPropertyName="isRepeatCorrect" originalPassword={passwordData.newPassword}
        input={passwordData.repeatPassword} setIsCorrect={setCorrects} setInput={setPasswordInfo}/>
        <div className={styles.submit}>
          <FormSubmit isActive={corrects.isPasswordCorrect && corrects.isRepeatCorrect && passwordData.oldPassword.length > 0 && !isPending} value={submitValue}/>
        </div>
        {!isSuccess ? <FormErrorMessage message={message}/> : <></>}
        {isShowingSuccess && <SuccessContainer message="Пароль успешно изменён"/>}
      </form>
  )
}
