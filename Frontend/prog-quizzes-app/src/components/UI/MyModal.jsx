
import MyInput from "./input/MyInput.jsx"
import MyButton from "./button/MyButton.jsx"
import React from 'react'
import { forwardRef } from "react"
import "./my_modal.css"
import PostForm from "../PostForm.jsx"

export default forwardRef(function MyModal(props, ref) {
  return (
    <>
        <div ref={ref} className="overlay" onClick={() => props.setModal(false)}>
            <div className="modal" onClick={(e) => e.stopPropagation()}>
                <PostForm addPost={props.addPost}></PostForm>
            </div>
        </div>
    </>
  )
})
