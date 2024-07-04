import React, { useEffect, useState } from 'react'
import Helper from '../../../services/Helper';
import FormField from '../form_field/FormField';

export default function PhoneField({input, propertyName, setInput, correctPropertyName, setIsCorrect, defaultValue=""}) {

    const [hint, setHint] = useState("");

    const validatePhone = () => {
        if (input === 0 || input === "") {
            setHint("");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
            return;
        }
        Helper.updateFieldData(Helper.phoneRegex, input, "Некорректный номер телефона", setHint, correctPropertyName, setIsCorrect);
    }

    useEffect(() => {
        validatePhone();
    }, [input])

  return (
    <FormField propertyName={propertyName} type={Helper.inputNumberType} placeholder="89349665345" label="Телефон (8...)" hint={hint} setInput={setInput}
    defaultValue={defaultValue}/>
  )
}
