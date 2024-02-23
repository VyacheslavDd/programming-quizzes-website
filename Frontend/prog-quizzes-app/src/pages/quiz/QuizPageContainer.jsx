
import React from 'react'
import { Provider } from 'react-redux'
import { store } from '../../redux/stores/QuizStore'
import QuizPage from './QuizPage'

export default function QuizPageContainer() {
  return (
    <Provider store={store}>
        <QuizPage/>
    </Provider>
  )
}
