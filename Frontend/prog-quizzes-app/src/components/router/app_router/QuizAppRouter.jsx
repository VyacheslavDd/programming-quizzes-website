import React, { useContext } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Header from '../../sections/Header/Header'
import MainPage from '../../../pages/main_page/MainPage'
import { authorizedRoutes, defaultRoutes } from './Routes'
import Footer from '../../sections/Footer/Footer'
import { AuthContext } from '../../../context/AuthContext'
import AuthMiddleware from '../../../middlewares/AuthMiddleware'

export default function QuizAppRouter() {

  const { token } = useContext(AuthContext);

  return (
      <BrowserRouter>
          <AuthMiddleware>
            <Header/>
            <Routes>
              {token === ""
              ? 
                defaultRoutes.map(route => 
                    <Route key={route.path} path={route.path} element={<route.element/>} exact={route.exact}/>)
              : authorizedRoutes.map(route => 
                <Route key={route.path} path={route.path} element={<route.element/>} exact={route.exact}/>)
              }
            </Routes>
            <Footer/>
          </AuthMiddleware>
      </BrowserRouter>
  )
}
