import { useState } from "react"
import "./app.css"
import QuizAppRouter from "./components/router/app_router/QuizAppRouter"
import { AuthContext } from "./context/AuthContext"

function App() {
  
  const [token, setToken] = useState("xd");

  return(
    <AuthContext.Provider value={{token, setToken}}>
      <QuizAppRouter/>
    </AuthContext.Provider>
  )
}

export default App
