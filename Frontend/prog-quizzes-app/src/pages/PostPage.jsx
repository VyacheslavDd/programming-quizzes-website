
import React, { useEffect, useRef, useState } from 'react'
import { useParams } from 'react-router-dom'
import PostService from '../API/PostService';
import styles from "./PostPage.module.css"

export default function () {

    const {id} = useParams();
    const [title, setTitle] = useState("");
    const [body, setBody] = useState("");
    const [comments, setComments] = useState([]);

    useEffect(() => {
      async function getPostData() {
        let response = await PostService.getPostById(id);
        setTitle(response.title);
        setBody(response.body);
      }
      async function getComments() {
        let response = await PostService.getCommentsToPost(id);
        setComments(response);
      }
      getPostData();
      getComments();
    }, [])

    return (
    <div className={styles.main}>
        <h1 style={{textAlign: "center", marginBottom: "30px"}}>Страница поста {id}</h1>
        <div className={styles.wrapper}>
          <div className={styles.post_block}>
              <h2>{title}</h2>
              <p>{body}</p>
          </div>
        </div>
        <h1 style={{textAlign: "center", marginBottom: "30px", marginTop: "30px"}}>Комментарии</h1>
        <ul>
          {comments.map((comm, index) => <li key={index}>{comm.email} {comm.name}</li>)}
        </ul>
        </div>
  )
}
