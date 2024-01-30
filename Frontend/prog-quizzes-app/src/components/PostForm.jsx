import MyInput from './UI/input/MyInput'
import MyButton from './UI/button/MyButton'
import React from 'react'
import { useState } from 'react';

export default function PostForm(props) {

    const [post, setPost] = useState({title: '', body: ''});

    const addNewPost = (e) => {
        e.preventDefault();
        props.addPost(post);
        setPost({title: '', body: ''});
    }

    return (
        <form>
            <MyInput type="text" placeholder="Введите название..." value={post.title} onChange={e => setPost({...post, title: e.target.value})}></MyInput>
            <MyInput type="text" placeholder="Введите описание..." value={post.body} onChange={e => setPost({...post, body: e.target.value})}></MyInput>
            <MyButton onClick={addNewPost}>Создать пост</MyButton>
        </form>
  )
}
