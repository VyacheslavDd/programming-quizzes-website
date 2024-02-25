import PaginationButton from "../pagination_button/PaginationButton";
import styles from "./PaginationRow.module.css"
import React, { useEffect, useState } from 'react'

export default function PaginationRow({count, onPaginationClick, currentPage, markedPages}) {

    const [pages, setPages] = useState([]);
    const [hasMarkedPages, setHasMarkedPages] = useState(false);
    useEffect(() => {
        setPages([...Array(count).keys()]);
        if (typeof markedPages !== "undefined") {
            setHasMarkedPages(true);
        }
    }, [count])

  return (
    <div className={styles.paginationRow}>
        {pages.map(page => <PaginationButton isCurrent={page === currentPage ? true : false}
        onClick={() => onPaginationClick(page)} key={page}>{hasMarkedPages && markedPages[page] ? "✔" : `${page + 1}`}</PaginationButton>)}
    </div>
  )
}
