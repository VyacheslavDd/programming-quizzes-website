import { useEffect, useState } from "react"
import "./app.css"
import QuizAppRouter from "./components/router/app_router/QuizAppRouter"
import { AuthContext } from "./context/AuthContext"
import Helper from "./services/Helper";
import { jwtDecode } from "jwt-decode";
import TokenHelper from "./services/TokenHelper";

function App() {

  const [token, setToken] = useState("");

  useEffect(() => {
    async function pullToken() {
      setToken(localStorage.getItem(TokenHelper.tokenStorageKey));
      await TokenHelper.tryRefreshToken(setToken);
    }
    pullToken();
  }, [])

  return(
    <AuthContext.Provider value={{token, setToken}}>
      <QuizAppRouter />
    </AuthContext.Provider>
  )
}

export default App
