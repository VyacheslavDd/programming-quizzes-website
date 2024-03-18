import styles from "./SearchInput.module.css"
import React, { useEffect, useRef } from 'react'

export default function SearchInput({placeholder, onInput, maxLength=30, startWidth="500px"}) {

    const onInputChange = (e) => {
        e.target.style.width = ((e.target.value.length + 5) * 8) + "px";
        onInput(e.target.value);
    }

    const inputRef = useRef();

    useEffect(() => {
        inputRef.current.style.width = startWidth;
    }, [inputRef])

  return (
    <div>
        <input ref={inputRef} maxLength={maxLength} className={styles.input} type="text" placeholder={placeholder} onInput={(e) => onInputChange(e)}/>
    </div>
  )
}
