import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function LoginField({input, setInput, setIsCorrect}) {

  const [hint, setHint] = useState("");

  const validateLogin = () => {
    if (input === "") {
      setHint("");
      setIsCorrect(false);
      return;
    }
    Helper.updateFieldData(Helper.loginRegex, input, "Логин должен быть от 8 до 20 символов;\nНачинаться с буквы;\nСодержать A-Z, a-z, _",
    setHint, setIsCorrect)
  }

  useEffect(() => {
    validateLogin();
  }, [input])

  return (
    <FormField type={Helper.inputTextType} placeholder="Corrupted_34" label="Логин" hint={hint} setInput={setInput}/>
  )
}
