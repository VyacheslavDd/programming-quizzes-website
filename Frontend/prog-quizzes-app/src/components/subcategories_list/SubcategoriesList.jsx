import Subcategory from "../subcategory/Subcategory"
import styles from "./SubcategoriesList.module.css"
import React from 'react'

export default function SubcategoriesList({subcategories}) {
  return (
    <div className={styles.subcategories}>
        {subcategories.map(subcategory => <Subcategory key={subcategory.id} name={subcategory.name}/>)}
    </div>
  )
}
