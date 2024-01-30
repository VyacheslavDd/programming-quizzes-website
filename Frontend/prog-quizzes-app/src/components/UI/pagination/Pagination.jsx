import styles from "./Pagination.module.css"
import React from 'react'
import MyButton from '../button/MyButton';

export default function Pagination({pagesCount, postsPage, setPostsPage}) {

    const paginationButtons = [];
    for (let i = 1; i <= pagesCount; i++) {
        paginationButtons.push(<MyButton style={postsPage === i ? {border: "2px solid yellow"} : {}} onClick={() => setPostsPage(i)}>{i}</MyButton>)
    }
    return (
    <>
        {paginationButtons.length > 0 
        ?
            <ul className={styles.pagButtonsList}>
                {paginationButtons.map((pb, index) => <li className={styles.pagButtonsItem} key={index}>{pb}</li>)}
            </ul>
        : <></>}
    </>
  )
}
