import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { signoutRedirectCallback } from '../../auth/userService';

function SignoutOIDC() {
    const navigate = useNavigate();
    useEffect(() => {
        const signoutAsync = async () => {
            await signoutRedirectCallback();
            navigate('/');
        };
        signoutAsync();
    }, [navigate]);
    return <div>Redirecting...</div>;
}

export default SignoutOIDC;
