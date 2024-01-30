import { BrowserRouter, Route, Routes, useParams } from "react-router-dom"
import Posts from "./pages/posts/Posts"
import Navbar from "./components/UI/navbar/Navbar"
import Standard from "./pages/standard/Standard"
import About from "./pages/about/About"
import PostPage from "./pages/PostPage"
import AppRouter from "./routing/AppRouter"
import {AuthContext} from "./contexts/AuthContext"
import { useEffect, useState } from "react"

function App() {

  const [isAuth, setIsAuth] = useState(false);

  useEffect(() => {
    if (localStorage.getItem('isAuth')) {
      setIsAuth(true);
    }
  }, []);

  return(
    <>
      <AuthContext.Provider value={{isAuth: isAuth, setIsAuth: setIsAuth}}>
        <BrowserRouter>
        <Navbar/>
          <AppRouter/>
        </BrowserRouter>
      </AuthContext.Provider>
    </>
  )
}

export default App
