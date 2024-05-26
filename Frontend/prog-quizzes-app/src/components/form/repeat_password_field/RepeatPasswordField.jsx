import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function RepeatPasswordField({input, originalPassword, setInput, setIsCorrect}) {

    const [hint, setHint] = useState("");
    
    const validatePassword = () => {
        if (input === "") {
            setHint("");
            setIsCorrect(false);
            return;
        }
        if (originalPassword === input) {
            setHint("");
            setIsCorrect(true);
        }
        else {
            setHint("Пароли не совпадают");
            setIsCorrect(false);
        }
    }

    useEffect(() => {
        validatePassword();
    }, [input])

  return (
    <FormField type={Helper.inputPasswordType} placeholder="Повторите пароль..." label="Подтверждение пароля" hint={hint} setInput={setInput}/>
  )
}
