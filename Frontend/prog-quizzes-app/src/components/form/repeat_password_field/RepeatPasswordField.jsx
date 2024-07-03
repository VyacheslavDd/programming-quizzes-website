import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function RepeatPasswordField({input, propertyName, originalPassword, setInput, correctPropertyName, setIsCorrect}) {

    const [hint, setHint] = useState("");
    
    const validatePassword = () => {
        if (input === "") {
            setHint("");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
            return;
        }
        if (originalPassword === input) {
            setHint("");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: true}));
        }
        else {
            setHint("Пароли не совпадают");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
        }
    }

    useEffect(() => {
        validatePassword();
    }, [input])

  return (
    <FormField propertyName={propertyName} type={Helper.inputPasswordType} placeholder="Повторите пароль..." label="Подтверждение пароля" hint={hint} setInput={setInput}/>
  )
}
