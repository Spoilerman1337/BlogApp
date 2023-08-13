import { User, UserManager, UserManagerSettings } from 'oidc-client-ts';
import { setAuthHeader } from './authHeaders';

const userManagerSettings: UserManagerSettings = {
    authority: 'https://localhost:7090/',
    client_id: 'blog-app-client',
    redirect_uri: 'http://localhost:5000/signin-oidc',
    response_type: 'code',
    scope: 'openid profile email BlogAppWebAPI BlogAppAuthWebAPI',
    post_logout_redirect_uri: 'http://localhost:5000/signout-oidc', 
    client_secret: '584a7aea-d8ff-4b7c-9937-3b3e992708eb'
};
  
const userManager = new UserManager(userManagerSettings);
export async function loadUser(): Promise<User | null> {
    const user = await userManager.getUser();
    const token = user?.access_token;
    setAuthHeader(token);

    return user;
}

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => userManager.signinRedirectCallback();

export const signoutRedirect = (args?: any) => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirect(args);
};

export const signoutRedirectCallback = () => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
};

export default userManager;