import React, { useEffect, useState } from 'react'
import FormField from '../form_field/FormField'
import Helper from '../../../services/Helper'

export default function TextField({input, hintMessage, label, pattern, propertyName, setInput, correctPropertyName, setIsCorrect, defaultValue=""}) {

    const [hint, setHint] = useState("");

    const validateTextField = () => {
        if (input === "") {
            setHint("");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
            return;
        }
        Helper.updateFieldData(pattern, input, hintMessage, setHint, correctPropertyName, setIsCorrect)
    }

    useEffect(() => {
        validateTextField();
    }, [input])

  return (
    <FormField type={Helper.inputTextType} label={label} placeholder={`${label}...`} propertyName={propertyName} defaultValue={defaultValue}
    setInput={setInput} hint={hint}/>
  )
}
