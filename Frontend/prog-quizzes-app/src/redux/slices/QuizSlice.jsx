
import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    questionsCount: 0,
    currentQuestion: 0,
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
            const questions = action.payload;
            let counter = 0;
            for (let question of questions) {
                state.questionAnswers[counter] = {};
                for (let answer of question.answers) {
                    state.questionAnswers[counter][answer.name] = false;
                }
                counter++;
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
            state.answeredQuestions[questionNumber] = true;
        },
        resetState: (state) => {
            Object.assign(state, initialState);
        }
    }
})

export const {setQuestionsCount, setCurrentQuestion, setQuestionAnswersInfo,
     updateQuestionAnswersInfo, updateAnsweredQuestionsInfo, resetState} = quizSlice.actions;
export default quizSlice.reducer;