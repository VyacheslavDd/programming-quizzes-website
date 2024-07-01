import React from 'react'
import styles from "./CabinetMain.module.css"

export default function CabinetMain({children}) {
  return (
    <main className={styles.mainBlock}>
        {children}
    </main>
  )
}
