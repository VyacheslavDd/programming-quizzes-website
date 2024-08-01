
import React, { useContext, useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import { AuthContext } from '../context/AuthContext';
import { jwtDecode } from 'jwt-decode';
import Helper from '../services/Helper';
import TokensAPI from '../services/API/TokensAPI';

export default function AuthMiddleware({children}) {
    
    const location = useLocation();
    const { token, setToken } = useContext(AuthContext);
    const [authToken, setAuthToken] = useState(null);
 

    useEffect(() => {
        async function checkTokenForNewOne() {
            if (authToken !== null) {
                if (Helper.isTokenExpired(authToken)) {
                    try {
                        let result = await TokensAPI.refreshTokens(authToken['Id']);
                        if (result.accessToken === "") throw new Error();
                        setToken(result.accessToken);
                        setAuthToken(jwtDecode(result.accessToken));
                    }
                    catch {
                        setToken("");
                        localStorage.removeItem(Helper.tokenStorageKey);
                        setAuthToken(null);
                    }
                }
            }
        }
        checkTokenForNewOne();
    }, [location])

    useEffect(() => {
        if (token !== "") {
            let decodedToken = jwtDecode(token);
            setAuthToken(decodedToken);
        }
    }, [token])

    return children;
}
