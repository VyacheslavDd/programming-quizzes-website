import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function EmailField({input, setInput, setIsCorrect}) {

    const [hint, setHint] = useState("");

    const validateEmail = () => {
        if (input === "") {
            setHint("");
            setIsCorrect(false);
            return;
        }
        Helper.updateFieldData(Helper.emailRegex, input, "Некорректный адрес почты", setHint, setIsCorrect);
    }

    useEffect(() => {
        validateEmail();
    }, [input])

  return (
    <FormField type={Helper.inputTextType} placeholder="quizz@mail.ru" label="Почта" hint={hint} setInput={setInput}/>
  )
}
