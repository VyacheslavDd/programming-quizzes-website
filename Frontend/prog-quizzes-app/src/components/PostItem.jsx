
import { useNavigate } from "react-router-dom"
import MyButton from "./UI/button/MyButton"

function PostItem(props) {

    const router = useNavigate();

    return(
        <div className="post">
            <div className="post_content">
                <strong>{props.number}. {props.post.title}</strong>
                <div>
                    {props.post.body}
                </div>
            </div>
            <div className="post_btns">
                <MyButton onClick={() => router(`/posts/${props.post.id}`)}>Открыть</MyButton>
            </div>
            <div className="post_btns">
                <MyButton onClick={() => props.remove(props.post)}>Удалить</MyButton>
            </div>
      </div>
    )
}

export default PostItem