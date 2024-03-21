import PaginationButton from "../pagination_button/PaginationButton";
import styles from "./PaginationRow.module.css"
import React, { useEffect, useState } from 'react'

export default function PaginationRow({count, onPaginationClick, currentPage, markedPages, showOnly}) {

    const [pages, setPages] = useState([]);
    const [hasMarkedPages, setHasMarkedPages] = useState(false);

    const fillPagination = () => {
      if (typeof showOnly === "undefined") {
        setPages([...Array(count).keys()]);
      }
      else {
        let pages = [];
        for (let i = currentPage - showOnly; i < currentPage; i++) {
          if (i >= 0) {
            pages.push(i);
          }
        }
        for (let i = currentPage; i <= currentPage + showOnly; i++) {
          if (i < count) {
            pages.push(i);
          }
        }
        setPages(pages);
      }
    }

    useEffect(() => {
        fillPagination();
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
