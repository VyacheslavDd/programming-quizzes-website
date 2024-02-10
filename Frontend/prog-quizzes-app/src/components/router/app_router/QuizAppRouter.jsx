import React from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Header from '../../sections/Header/Header'
import MainPage from '../../../pages/main_page/MainPage'
import { routes } from './Routes'
import Footer from '../../sections/Footer/Footer'

export default function QuizAppRouter() {
  return (
    <BrowserRouter>
        <Header/>
        <Routes>
            {routes.map(route => 
                <Route key={route.path} path={route.path} element={<route.element/>} exact={route.exact}/>)}
            <Route path='*' element={<MainPage/>}/>
        </Routes>
        <Footer/>
  </BrowserRouter>
  )
}
