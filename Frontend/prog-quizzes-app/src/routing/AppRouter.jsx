import { Routes, Route } from 'react-router-dom'
import React, { useContext } from 'react'
import Standard from '../pages/standard/Standard'
import Posts from '../pages/posts/Posts'
import PostPage from '../pages/PostPage'
import About from '../pages/about/About'
import { PrivateRoutes, PublicRoutes } from './routes'
import Login from '../pages/login/Login'
import { AuthContext } from '../contexts/AuthContext'

export default function AppRouter() {
    const isLogin = useContext(AuthContext)['isAuth'];
    return (
          isLogin
          ?
          <Routes>
          {PrivateRoutes.map((route, index) => <Route key={index} path={route.path} element={<route.element/>} exact={route.exact}></Route>)}
          <Route path='*' element={<Posts/>}></Route>
          </Routes>
          :
          <Routes>
          {PublicRoutes.map((route, index) => <Route key={index} path={route.path} element={<route.element/>}></Route>)}
          <Route path="*" element={<Login/>}/>
          </Routes>
  )
}
