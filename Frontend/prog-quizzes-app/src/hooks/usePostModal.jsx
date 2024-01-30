import { useEffect, useMemo } from "react";


export function usePostModal(showPostModal, modalRef) {
    useEffect(() => {
        if (showPostModal) {
          modalRef.current.style.display = "flex";
        }
        else {
          modalRef.current.style.display = "none";
        }
      }, [showPostModal])
}

export function useSortedPosts(posts, sortOption) {
    return useMemo(() => {
        function getSortedPosts() {
          if (sortOption !== "") {
            return [...posts].sort((a, b) => a[sortOption].localeCompare(b[sortOption]));
          }
          return posts;
        }
        return getSortedPosts();
    }, [sortOption, posts]);
}
