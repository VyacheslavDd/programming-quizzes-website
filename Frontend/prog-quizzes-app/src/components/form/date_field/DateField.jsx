import React, { useEffect, useRef, useState } from 'react'
import FormField from '../form_field/FormField';
import Helper from '../../../services/Helper';

export default function DateField({input, propertyName, setInput, correctPropertyName, setIsCorrect, defaultValue}) {

    const date = useRef(new Date());
    const fieldDate = useRef(date.current.toISOString().match(Helper.dateRegex)[0]);
    const defaultDate = useRef(defaultValue.match(Helper.dateRegex)[0]);
    const [hint, setHint] = useState("");

    const validateDate = () => {
        let dateObj = new Date(input);
        let year = dateObj.getFullYear();
        if (input === "" || year < 1970 || dateObj > date.current) {
            setHint("Выбрана некорректная дата");
            setIsCorrect((prev) => ({...prev, [correctPropertyName]: false}));
            return;
        }
        setHint("");
        setIsCorrect((prev) => ({...prev, [correctPropertyName]: true}));
    }

    useEffect(() => {
        validateDate();
    }, [input])

  return (
    <FormField propertyName={propertyName} type={Helper.inputDateType} placeholder="" label="Дата рождения" hint={hint} setInput={setInput}
    defaultValue={defaultDate.current} min="1970-01-01" max={fieldDate.current}/>
  )
}
