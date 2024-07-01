import React, { useEffect, useRef, useState } from 'react'
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

export default function CabinetPasswordControl({user}) {
  const [passwordData, setPasswordData] = useState({oldPassword: "", newPassword: "", repeatPassword: ""});
  const [isPasswordCorrect, setIsPasswordCorrect] = useState(false);
  const [isRepeatCorrect, setIsRepeatCorrect] = useState(false);
  const [isShowingSuccess, setIsShowingSuccess, doTimeout] = useSuccessTimeout(3000);
  const [submitValue, isPending, isSuccess, message, submit] = useFormSubmit("Сменить пароль", "Смена пароля...", async () => {
    return await UserAPI.updatePassword(user.id, passwordData);
  });
  const updatePassword = async (e) => {
    e.preventDefault();
    await submit();
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
        <FormField type={Helper.inputPasswordType} label="Старый пароль" placeholder="Введите пароль..."
        setInput={(value) => setPasswordData({...passwordData, oldPassword: value})}/>
        <PasswordField input={passwordData.newPassword} setInput={(value) => setPasswordData({...passwordData, newPassword: value})} setIsCorrect={setIsPasswordCorrect}/>
        <RepeatPasswordField originalPassword={passwordData.newPassword} input={passwordData.repeatPassword} setIsCorrect={setIsRepeatCorrect}
          setInput={(value) => setPasswordData({...passwordData, repeatPassword: value})}/>
        <div className={styles.submit}>
          <FormSubmit isActive={isPasswordCorrect && isRepeatCorrect && passwordData.oldPassword.length > 0 && !isPending} value={submitValue}/>
        </div>
        {!isSuccess ? <FormErrorMessage message={message}/> : <></>}
        {isShowingSuccess && <span className={styles.updateSuccess}>Пароль успешно изменён</span>}
      </form>
  )
}
