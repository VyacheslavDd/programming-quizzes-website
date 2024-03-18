import useFetching from "../../../hooks/useFetching"
import ImageAPI from "../../../services/API/ImageAPI";
import Helper from "../../../services/Helper";
import AnswerSelect from "../answer_select/AnswerSelect";
import styles from "./QuizQuestion.module.css"
import React, { useEffect, useState } from 'react'

export default function QuizQuestion({question, imagesInfo, setImagesInfo}) {

  const [image, setImage] = useState("");
  const [fetchImage, isLoading, isError] = useFetching(async () => {
    let imageBytes = await ImageAPI.getQuestionImageAsync(question.imageUrl);
    setImage(imageBytes);
    setImagesInfo(prev => ({...prev, [question.imageUrl]: imageBytes}));
  })

  useEffect(() => {
    if (imagesInfo[question.imageUrl] === undefined) {
      fetchImage();
    }
    else {
      setImage(imagesInfo[question.imageUrl]);
    }
  }, [question]);

  return (
    <>
        <img width={500} height={300} className={styles.questionImage} src={`data:image;base64,${image}`} alt="Картинка к вопросу"></img>
        <h2 className={styles.title}>{question.title}</h2>
        <p className={styles.description}>{question.description}</p>
        <AnswerSelect answers={question.answers} type={Helper.getInputType(question.questionType)}/>
    </>
  )
}
