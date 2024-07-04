import React, { useEffect, useState } from 'react'
import styles from "./CabinetMainInfo.module.css"
import Form from '../../../components/form/Form'
import FormImage from '../../../components/form/form_image/FormImage'
import userLogo from "../../../assets/user-default.png"
import EmailField from '../../../components/form/email_field/EmailField'
import FormSubmit from '../../../components/form/form_submit/FormSubmit'
import LoginField from '../../../components/form/login_field/LoginField'
import TextField from '../../../components/form/text_field/TextField'
import Helper from '../../../services/Helper'
import PhoneField from '../../../components/form/phone_field/PhoneField'
import DateField from '../../../components/form/date_field/DateField'
import useFormSubmit from '../../../hooks/useFormSubmit'
import UserAPI from '../../../services/API/UserAPI'
import useSuccessTimeout from '../../../hooks/useSuccessTimeout'
import FormErrorMessage from '../../../components/form/error_message/FormErrorMessage'
import SuccessContainer from '../../../components/success_container/SuccessContainer'

export default function CabinetMainInfo({user, setUser}) {

    const [avatar, setAvatar] = useState(null);
    const [corrects, setCorrects] = useState({isNameCorrect: false, isSurnameCorrect: false, isBirthDateCorrect: false, isPhoneCorrect: false,
    isEmailCorrect: false, isLoginCorrect: false});
    const [submitValue, isPending, isSuccess, message, submit] = useFormSubmit("Сохранить", "Сохраняем данные...", async () => {
      return await UserAPI.updateUser(user);
    });
    const [isShowingSuccess, setIsShowingSuccess, doTimeout] = useSuccessTimeout(3000);

    const canSubmit = () => {
      for (let value of Object.values(corrects)) {
        if (!value) return false;
      }
      return !isPending;
    }

    const updateUser = async (e) => {
      e.preventDefault();
      let result = await submit();
      console.log(result);
    }

  useEffect(() => {
    setAvatar(user.avatar === undefined || user.avatar === "" ? userLogo : user.avatar);
  }, [])

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
    <form action='post' className={styles.userInfo} onSubmit={(e) => updateUser(e)}>
        <div className={styles.infoInputs}>
          <div className={styles.infoDiv}>
            <FormImage avatar={avatar} setAvatar={setAvatar}/>
          </div>
          <div className={styles.infoDiv}>
            <TextField input={user.name} correctPropertyName="isNameCorrect" hintMessage="Длина от 2 до 15 символов, используйте только буквы" label="Имя"
            propertyName="name" setInput={setUser} setIsCorrect={setCorrects} defaultValue={user.name} pattern={Helper.nameRegex}/>
            <TextField input={user.surname} correctPropertyName="isSurnameCorrect" hintMessage="Длина от 2 до 20 символов, используйте только буквы" label="Фамилия"
            propertyName="surname" setInput={setUser} setIsCorrect={setCorrects} defaultValue={user.surname} pattern={Helper.surnameRegex}/>
            <LoginField propertyName="login" correctPropertyName="isLoginCorrect" input={user.login} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.login}/>
          </div>
          <div className={styles.infoDiv}>
            <EmailField propertyName="email" correctPropertyName="isEmailCorrect" input={user.email} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.email}/>
            <PhoneField propertyName="phoneNumber" correctPropertyName="isPhoneCorrect" input={user.phoneNumber} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.phoneNumber > 0 ? user.phoneNumber : ""}/>
            <DateField propertyName="birthDate" correctPropertyName="isBirthDateCorrect" input={user.birthDate} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.birthDate}/>
          </div>
        </div>
        <div className={styles.submit}>
          <FormSubmit isActive={canSubmit()} value={submitValue}/>
        </div>
        {!isSuccess && <FormErrorMessage message={message}/>}
        {isShowingSuccess && <SuccessContainer message="Данные успешно обновлены"/>}
    </form> 
  )
}
