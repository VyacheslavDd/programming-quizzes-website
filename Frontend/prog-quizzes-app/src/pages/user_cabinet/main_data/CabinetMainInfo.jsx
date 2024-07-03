import React, { useEffect, useState } from 'react'
import styles from "./CabinetMainInfo.module.css"
import Form from '../../../components/form/Form'
import FormImage from '../../../components/form/form_image/FormImage'
import userLogo from "../../../assets/user-default.png"
import EmailField from '../../../components/form/email_field/EmailField'
import FormSubmit from '../../../components/form/form_submit/FormSubmit'
import LoginField from '../../../components/form/login_field/LoginField'

export default function CabinetMainInfo({user, setUser}) {

  const [avatar, setAvatar] = useState(null);
  const [corrects, setCorrects] = useState({isNameCorrect: false, isSurnameCorrect: false, isBirthDateCorrect: false, isPhoneCorrect: false,
    isEmailCorrect: false, isLoginCorrect: false});

  useEffect(() => {
    setAvatar(user.avatar === undefined || user.avatar === "" ? userLogo : user.avatar);
  }, [])

  return (
    <form action='post' className={styles.userInfo}>
        <div className={styles.infoInputs}>
          <div className={styles.infoDiv}>
            <FormImage avatar={avatar} setAvatar={setAvatar}/>
          </div>
          <div className={styles.infoDiv}>
            <LoginField propertyName="login" correctPropertyName="isLoginCorrect" input={user.login} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.login}/>
          </div>
          <div className={styles.infoDiv}>
            <EmailField propertyName="email" correctPropertyName="isEmailCorrect" input={user.email} setInput={setUser} setIsCorrect={setCorrects}
            defaultValue={user.email}/>
          </div>
        </div>
        <div className={styles.submit}>
          <FormSubmit isActive={true} value="Сохранить"/>
        </div>
    </form> 
  )
}
