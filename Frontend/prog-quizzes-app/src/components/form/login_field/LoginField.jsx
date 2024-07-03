import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function LoginField({input, propertyName, setInput, setIsCorrect, correctPropertyName, defaultValue=""}) {

  const [hint, setHint] = useState("");

  const validateLogin = () => {
    if (input === "") {
      setHint("");
      setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
      return;
    }
    Helper.updateFieldData(Helper.loginRegex, input, "Логин должен быть от 8 до 20 символов;\nНачинаться с буквы;\nСодержать A-Z, a-z, _",
    setHint, correctPropertyName, setIsCorrect)
  }

  useEffect(() => {
    validateLogin();
  }, [input])

  return (
    <FormField propertyName={propertyName} type={Helper.inputTextType} placeholder="Corrupted_34" label="Логин" hint={hint} setInput={setInput}
    defaultValue={defaultValue}/>
  )
}
