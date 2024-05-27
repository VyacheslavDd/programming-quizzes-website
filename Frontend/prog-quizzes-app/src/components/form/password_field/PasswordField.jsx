import React, { useEffect, useRef, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function PasswordField({input, setInput, setIsCorrect}) {

    const [hint, setHint] = useState("");

    const validatePassword = () => {
        if (input === "") {
            setHint("");
            setIsCorrect(false);
            return;
        }
        Helper.updateFieldData(Helper.passwordRegex, input, "Пароль должен быть длиной от 8 до 15 символов и содержать латинские буквы, цифры, символы @~!%&_",
            setHint, setIsCorrect)
    }

    useEffect(() => {
        validatePassword();
    }, [input])

  return (
    <FormField type={Helper.inputPasswordType} placeholder="Введите пароль..." label="Пароль" setInput={setInput} hint={hint}/>
  )
}
