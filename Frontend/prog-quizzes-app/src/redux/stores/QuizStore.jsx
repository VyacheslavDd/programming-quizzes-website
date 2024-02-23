import { configureStore } from "@reduxjs/toolkit";
import QuizSlice from "../slices/QuizSlice";

export const store = configureStore({
    reducer: QuizSlice
});