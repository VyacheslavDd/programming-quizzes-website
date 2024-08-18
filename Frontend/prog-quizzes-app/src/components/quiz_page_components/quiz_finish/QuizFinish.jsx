import { useDispatch, useSelector } from "react-redux"
import styles from "./QuizFinish.module.css"
import React, { useEffect, useRef, useState } from 'react'
import { resetState, setQuestionsCount, setQuestionAnswersInfo, setAnsweredQuestionsInfo } from "../../../redux/slices/QuizSlice";
import Helper from "../../../services/Helper";
import GenericButton from "../../UI/buttons/generic_button/GenericButton";
import { useNavigate } from "react-router-dom";
import QuestionResume from "../question_resume/QuestionResume";
import OptionsSelect from "../../UI/options_select/OptionsSelect";
import QuizRatingAPI from "../../../services/API/QuizRatingAPI";

export default function QuizFinish({questions, setIsFinished, quizId, userId}) {

    const router = useNavigate();
    const quizDispatch = useDispatch();
    const [correctAnswersCount, setCorrectAnswersCount] = useState(0);
    const [onDetailsButton, setOnDetailsButton] = useState(false);
    const [doShowDetails, setDoShowDetails] = useState(false);
    const questionAnswers = useSelector(state => state.questionAnswers);
    const [questionsCorrect, setQuestionsCorrect] = useState({});
    const [userRating, setUserRating] = useState(0);
    const quizRatingSelectRef = useRef();
    
    const updateDetailsButton = (state) => {
        setOnDetailsButton(state);
    }

    const manageRating = async (e) => {
        if (e.target.value === "0" && userRating > 0) {
            await QuizRatingAPI.removeUserRatingAsync(quizId, userId, userRating);
        }
        if (e.target.value !== "0" && userRating === 0) {
            await QuizRatingAPI.addUserRatingAsync(quizId, userId, Number(e.target.value));
        }
        if (e.target.value !== "0" && userRating !== 0) {
            await QuizRatingAPI.updateUserRatingAsync(quizId, userId, Number(e.target.value));
        }
        setUserRating(Number(e.target.value));
    }

    const resetQuiz = () => {
        quizDispatch(resetState());
        quizDispatch(setQuestionsCount(questions.length));
        quizDispatch(setQuestionAnswersInfo(questions));
        quizDispatch(setAnsweredQuestionsInfo(questions.length));
        setIsFinished(false);
    }

    useEffect(() => {
        questions.forEach((question, index) => {
            let isCorrect = true;
            question.answers.forEach(answer => {
                if (questionAnswers[index][answer.name] !== answer.isCorrect) {
                    isCorrect = false;
                }
            });
            if (isCorrect) {
                setCorrectAnswersCount(prev => prev + 1);
            }
            setQuestionsCorrect(prev => ({...prev, [index]: isCorrect}));
        })
    }, [])

    useEffect(() => {
        async function getUserRating() {
            let rating = await QuizRatingAPI.getUserRatingAsync(quizId, userId);
            if (rating !== "") {
                quizRatingSelectRef.current.value = rating.rating;
                setUserRating(rating.rating);
            }
        }
        getUserRating();
    }, [])

    return (
        <>
            <h1 className={styles.finishMessage}>Викторина завершена!</h1>
            <span className={styles.resume} style={{color: Helper.defineQuizResumeColor(correctAnswersCount, questions.length)}}>
                Количество корректных ответов: {correctAnswersCount} из {questions.length} (x2 из-за react strict mode)
                </span>
            {!doShowDetails
            ? <button onClick={() => setDoShowDetails(true)} onMouseEnter={() => updateDetailsButton(true)} onMouseLeave={() => updateDetailsButton(false)}
            className={styles.detailsButton}>Посмотреть результаты? {onDetailsButton ? "↓↓" : ""}</button>
            : <>
                {questions.map((question, index) => <QuestionResume key={question.id}
                question={question} questionIndex={index} isCorrect={questionsCorrect[index]}/>)}
                <span className={styles.hint}>*Нижней чертой обозначены выбранные ответы</span>
                <button className={styles.detailsButton} onClick={() => setDoShowDetails(false)}>Скрыть результаты</button>
            </>}
            <div className={styles.rateQuiz}>
                <OptionsSelect label="Ваша оценка викторины:" options={Helper.rateOptions} onChange={(e) => manageRating(e)} ref={quizRatingSelectRef}/>
            </div>
            <div className={styles.buttonsRow}>
                <GenericButton onClick={() => router("/quizzes")}>К списку викторин</GenericButton>
                <GenericButton onClick={() => resetQuiz()}>Перепройти викторину</GenericButton>
            </div>
        </>
  )
}
