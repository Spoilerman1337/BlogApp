import React, { useEffect, FC } from 'react';
import { useNavigate } from 'react-router-dom';
import { signinRedirectCallback } from '../../auth/userService';

function SigninOIDC() {
    const navigate = useNavigate();
    useEffect(() => {
        async function signinAsync() {
            await signinRedirectCallback();
            navigate('/');
        }
        signinAsync();
    }, [navigate]);
    return <div>Redirecting...</div>;
}

export default SigninOIDC;
