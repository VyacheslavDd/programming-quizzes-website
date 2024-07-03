import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function EmailField({input, propertyName, setInput, correctPropertyName, setIsCorrect, defaultValue=""}) {

    const [hint, setHint] = useState("");

    const validateEmail = () => {
        if (input === "") {
            setHint("");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
            return;
        }
        Helper.updateFieldData(Helper.emailRegex, input, "Некорректный адрес почты", setHint, correctPropertyName, setIsCorrect);
    }

    useEffect(() => {
        validateEmail();
    }, [input])

  return (
    <FormField propertyName={propertyName} type={Helper.inputTextType} placeholder="quizz@mail.ru" label="Почта" hint={hint} setInput={setInput}
    defaultValue={defaultValue}/>
  )
}
