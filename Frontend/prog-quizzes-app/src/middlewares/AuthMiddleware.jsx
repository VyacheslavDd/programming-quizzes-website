
import React, { useContext, useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import { AuthContext } from '../context/AuthContext';
import { jwtDecode } from 'jwt-decode';
import Helper from '../services/Helper';

export default function AuthMiddleware({children}) {
    
    const location = useLocation();
    const { token, setToken } = useContext(AuthContext);
    const [authToken, setAuthToken] = useState(null);
 

    useEffect(() => {
        if (authToken !== null) {
            if (Helper.isTokenExpired(authToken)) {
                setToken("");
                localStorage.removeItem(Helper.tokenStorageKey);
                setAuthToken(null);
            }
        }
    }, [location])

    useEffect(() => {
        if (token !== "") {
            let decodedToken = jwtDecode(token);
            setAuthToken(decodedToken);
        }
    }, [token])

    return children;
}
