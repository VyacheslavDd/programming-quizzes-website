import React, { useRef } from 'react'
import styles from "./FormImage.module.css"
import userLogo from "../../../assets/user-default.png"
import GenericButton from '../../UI/buttons/generic_button/GenericButton';

export default function FormImage({avatar, setAvatar, setFile}) {

    const imageUploader = useRef(null);

    const uploadImage = (e) => {
        if (e.target.files) {
            const reader = new FileReader();
            reader.onload = (e) => {
                setAvatar(e.target.result);
            }
            reader.readAsDataURL(e.target.files[0]);
            setFile(e.target.files[0]);
        }
    }

    const deleteImage = (e) => {
        e.preventDefault();
        setAvatar(userLogo);
        setFile(null);
    }

    return (
        <div className={styles.filePicker}>
            <input ref={imageUploader} className={styles.fileInput} type='file' accept='image/*' multiple={false} onChange={(e) => uploadImage(e)}/>
            <div className={styles.imageContainer} onClick={() => imageUploader.current.click()}>
                <img src={avatar === userLogo ? userLogo : avatar} className={styles.image}/>
            </div>
            <div className={styles.removeImage}>
                <GenericButton disabled={avatar === userLogo} onClick={(e) => deleteImage(e)}>Удалить</GenericButton>
            </div>
        </div>
  )
}
