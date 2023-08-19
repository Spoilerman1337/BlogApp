import React, { ReactElement, useReducer, useEffect } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import styles from './styles/App.module.scss';
import LandingPage from './components/LandingPage';
import SigninOIDC from './components/auth/SigninOIDC';
import SignoutOIDC from './components/auth/SignoutOIDC';
import { loadUser } from './auth/userService';
import userReducer from './reducers/userReducer';

function App(): ReactElement {
    const [user, dispatch] = useReducer(userReducer, { user: null });

    useEffect(() => {
        const getUser = async () => {
            const userValue = await loadUser();
            dispatch({ type: 'set', payload: userValue });
            console.log(userValue);
        };
        getUser();
    }, []);

    return (
        <>
            <div className={styles.app}>
                <BrowserRouter>
                    <Routes>
                        <Route
                            path="/"
                            element={
                                <LandingPage isAuthenticated={user.user !== null} />
                            }
                        />
                        <Route path="/signin-oidc" element={<SigninOIDC />} />
                        <Route path="/signout-oidc" element={<SignoutOIDC />} />
                    </Routes>
                </BrowserRouter>
            </div>
        </>
    );
}

export default App;
