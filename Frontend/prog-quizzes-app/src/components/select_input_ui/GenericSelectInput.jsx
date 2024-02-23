import styles from "./GenericSelectInput.module.css"
import React from 'react'

export default function GenericSelectInput(props) {
  return (
    <>
        <label className={styles.container}>
            <input {...props} className={styles.input}/>
            <span className={styles.checkmark} style={props.type === "radio" ? {borderRadius: "20px"} : {}}></span>
        </label>
    </>
  )
}
