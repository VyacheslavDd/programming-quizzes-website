import { useEffect, useRef, useState } from "react"
import PostList from "../../components/PostList";
import "../../styles/app.css"
import MyButton from "../../components/UI/button/MyButton";
import MyModal from "../../components/UI/MyModal";
import { usePostModal, useSortedPosts } from "../../hooks/usePostModal";
import useFetching from "../../hooks/useFetching";
import PostService from "../../API/PostService";

function Posts() {
    const [posts, setPosts] = useState([
    ]);
    const totalPages = useRef(0);
    const limitPosts = useRef(5);
    const [postsPage, setPostsPage] = useState(1);
    const [sortOption, setSortOption] = useState("");
    const [searchValue, setSearchValue] = useState("");
    const [showPostModal, setShowPostModal] = useState(false);
    const [fetchPosts, arePostsLoading, error] = useFetching(async () => {
    const [posts, totalCount] = await PostService.getPage({_limit: limitPosts.current, _page: postsPage});
    totalPages.current = Math.ceil(totalCount / limitPosts.current);
    setPosts(posts);
    })
    const modalRef = useRef("");

    const sort = (sortField) => {
    if (sortField !== "") {
        setSortOption(prevSortOption => sortField);
    }
    }

    const addNewPost = (post) => {
    setPosts(prev => [...prev, {...post, id: Date.now()}]);
    setShowPostModal(false);
    }

    const removePost = (post) => {
    setPosts(posts.filter(p => p.id !== post.id))
    }

    const applySearchValue = (value) => {
    setSearchValue(value);
    }

    useEffect(() => {
    setPosts(prev => []);
    fetchPosts();
    }, [postsPage])

    usePostModal(showPostModal, modalRef);

    const sortedPosts = useSortedPosts(posts, sortOption);

    return(
    <>
        <MyButton onClick={() => setShowPostModal(true)} style={{marginBottom: "15px", marginTop: "15px"}}>Создать пост</MyButton>
        <MyModal ref={modalRef} addPost={addNewPost} setModal={setShowPostModal}></MyModal>
        {error && <h1>Произошла ошибка {error} при загрузке постов!</h1>}
        <PostList title="Список постов 1" posts={sortedPosts.filter(p => searchValue === "" || p.title.includes(searchValue))}
        remove={removePost} defaultValue="Сортировка" arePostsLoading={arePostsLoading}
        sortFieldValue={sortOption} onChangeFunc={sort} searchValue={searchValue} onChangeSearchFunc={applySearchValue}
        postsPage = {postsPage} setPostsPage={setPostsPage} pagesCount={totalPages.current}/>
    </>
)
}

export default Posts