import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Header from '../../sections/Header/Header'
import MainPage from '../../main_page/MainPage'
import About from '../../about/About'
import { routes } from './Routes'

export default function QuizAppRouter() {
  return (
    <BrowserRouter>
        <Header/>
        <Routes>
            {routes.map(route => 
                <Route key={route.path} path={route.path} element={<route.element/>} exact={route.exact}/>)}
            <Route path='*' element={<MainPage/>}/>
        </Routes>
  </BrowserRouter>
  )
}
