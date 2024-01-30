
import PostItem from "./PostItem"
import MyInput from "./UI/input/MyInput";
import MySelect from "./UI/select/MySelect"
import Loader from "./UI/animations/Loader";
import Pagination from "./UI/pagination/Pagination";

function PostList({posts, title, remove, defaultValue, sortFieldValue,
     onChangeFunc, searchValue, onChangeSearchFunc, arePostsLoading, setPostsPage, pagesCount, postsPage}) {

    const options = [{value: 'title', name: 'По названию'}, {value: 'body', name: 'По описанию'}];


    return(
        <>
            {posts.length > 0 ? <>
                <h1 style={{textAlign: "center"}}>{title}</h1>
                <MySelect
                options={[{value: 'title', name: 'По названию'}, {value: 'body', name: 'По описанию'}]}
                defaultValue={defaultValue}
                sortFieldValue={sortFieldValue}
                onChangeFunc={onChangeFunc}
                />
                <MyInput value={searchValue}  style={{width: "50%", textAlign: "center"}} onChange={(e) => onChangeSearchFunc(e.target.value)}
                 type="text" placeholder="Введите название поста..."></MyInput>
                {posts.map((post, index) => <PostItem number={index + 1} post={post} key={post.id} remove={remove}/>)}
                <Pagination setPostsPage={setPostsPage} pagesCount={pagesCount} postsPage={postsPage}/>
            </> : <>
                {!arePostsLoading
                ?
                <h1 style={{textAlign: "center"}}>
                    Список {title}: постов не найдено!
                    </h1>
                :<Loader/>}
                <MyInput value={searchValue}  style={{width: "50%", textAlign: "center"}} onChange={(e) => onChangeSearchFunc(e.target.value)}
                 type="text" placeholder="Введите название поста..."></MyInput>
            </>}
        </>
    )
}

export default PostList
