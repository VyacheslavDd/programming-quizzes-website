import React, { useEffect, useRef, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function LoginEmailField({input, setInput, setIsCorrect}) {

    const [hint, setHint] = useState("");
    const validateLoginEmailField = () => {
        if (input === "") {
            setHint("");
            setIsCorrect(false);
            return;
        }
        if (!input.includes("@")) {
            Helper.updateFieldData(Helper.loginRegex, input, "Логин должен быть от 8 до 20 символов;\nНачинаться с буквы;\nСодержать A-Z, a-z, _",
                setHint, setIsCorrect)
            //let testPassed = loginRegex.current.test(input);
            //setHint(testPassed ? "" : "Логин должен быть от 8 до 20 символов;\nНачинаться с буквы;\nСодержать A-Z, a-z, _");
            //setIsCorrect(testPassed ? true : false);
        }
        else {
            Helper.updateFieldData(Helper.emailRegex, input, "Некорректный адрес почты", setHint, setIsCorrect)
        }
    }

    useEffect(() => {
        validateLoginEmailField();
    }, [input]);

  return (
    <FormField type={Helper.inputTextType} placeholder='Corrupted_34' label="Логин или почта" setInput={setInput}
    hint={hint}/>
  )
}
