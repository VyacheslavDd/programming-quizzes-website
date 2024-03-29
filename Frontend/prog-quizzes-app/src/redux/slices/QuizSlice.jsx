
import { createSlice } from "@reduxjs/toolkit";
import Helper from "../../services/Helper";

const initialState = {
    questionsCount: 0,
    currentQuestion: 0,
    answeredQuestionsCount: 0,
    answeredQuestions: {},
    questionAnswers: {}
};

export const quizSlice = createSlice({
    name: "quizSlice",
    initialState: initialState,
    reducers: {
        setQuestionsCount: (state, action) => {
            state.questionsCount = action.payload;
        },
        setCurrentQuestion: (state, action) => {
            state.currentQuestion = action.payload;
        },
        setQuestionAnswersInfo: (state, action) => {
            const questions = Helper.shuffleArray(action.payload);
            let counter = 0;
            for (let question of questions) {
                state.questionAnswers[counter] = {};
                for (let answer of question.answers) {
                    state.questionAnswers[counter][answer.name] = false;
                }
                counter++;
            }
        },
        setAnsweredQuestionsInfo: (state, action) => {
            const count = action.payload;
            for (let i = 0; i < count; i++) {
                state.answeredQuestions[i] = false;
            }
        },
        updateQuestionAnswersInfo: (state, action) => {
            const [questionNumber, data, isRadio] = action.payload;
            if (isRadio) {
                for (let answer in state.questionAnswers[questionNumber]) {
                    state.questionAnswers[questionNumber][answer] = false;
                }
            }
            state.questionAnswers[questionNumber][data] = !state.questionAnswers[questionNumber][data];
        },
        updateAnsweredQuestionsInfo: (state, action) => {
            const questionNumber = action.payload;
            if (!state.answeredQuestions[questionNumber]) {
                state.answeredQuestionsCount++;
            }
            state.answeredQuestions[questionNumber] = true;
        },
        resetState: (state) => {
            Object.assign(state, initialState);
        }
    }
})

export const {setQuestionsCount, setCurrentQuestion, setQuestionAnswersInfo, setAnsweredQuestionsInfo,
     updateQuestionAnswersInfo, updateAnsweredQuestionsInfo, resetState} = quizSlice.actions;
export default quizSlice.reducer;