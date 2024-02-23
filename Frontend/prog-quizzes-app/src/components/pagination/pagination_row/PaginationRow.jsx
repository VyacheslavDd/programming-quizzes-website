import PaginationButton from "../pagination_button/PaginationButton";
import styles from "./PaginationRow.module.css"
import React, { useEffect, useState } from 'react'

export default function PaginationRow({count, onPaginationClick, currentPage}) {

    const [pages, setPages] = useState([]);
    useEffect(() => {
        setPages([...Array(count).keys()]);
    }, [count])

  return (
    <div className={styles.paginationRow}>
        {pages.map(page => <PaginationButton isCurrent={page === currentPage ? true : false}
        onClick={() => onPaginationClick(page)} key={page}>{page + 1}</PaginationButton>)}
    </div>
  )
}
