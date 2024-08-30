import React, { useContext } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Header from '../../sections/Header/Header'
import MainPage from '../../../pages/main_page/MainPage'
import { authorizedRoutes, defaultRoutes } from './Routes'
import Footer from '../../sections/Footer/Footer'
import { AuthContext } from '../../../context/AuthContext'
import Helper from '../../../services/Helper'
import TokenHelper from '../../../services/TokenHelper'

export default function QuizAppRouter() {

  const { token } = useContext(AuthContext);

  return (
      <BrowserRouter>
            <Header/>
            <Routes>
              {TokenHelper.isTokenInvalid(token) || TokenHelper.isTokenExpired(token)
              ? 
                defaultRoutes.map(route => 
                    <Route key={route.path} path={route.path} element={<route.element/>}/>)
              : authorizedRoutes.map(route => 
                <Route key={route.path} path={route.path} element={<route.element/>}/>)
              }
            </Routes>
            <Footer/>
      </BrowserRouter>
  )
}
