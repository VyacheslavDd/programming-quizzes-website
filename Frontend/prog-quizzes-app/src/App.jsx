import { useEffect, useState } from "react"
import "./app.css"
import QuizAppRouter from "./components/router/app_router/QuizAppRouter"
import { AuthContext } from "./context/AuthContext"
import Helper from "./services/Helper";
import { jwtDecode } from "jwt-decode";

function App() {
  
  const [token, setToken] = useState("");

  useEffect(() => {
    let storedToken = localStorage.getItem(Helper.tokenStorageKey);
    if (storedToken !== null) {
      let decodedToken = jwtDecode(storedToken);
      let isExpired = Helper.isTokenExpired(decodedToken);
      setToken(isExpired ? "" : storedToken);
    }
  }, [])

  return(
    <AuthContext.Provider value={{token, setToken}}>
      <QuizAppRouter/>
    </AuthContext.Provider>
  )
}

export default App
