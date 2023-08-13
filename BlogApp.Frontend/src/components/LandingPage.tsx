import React, { ReactNode } from 'react';
import LandingPageAuthenticated from './landing/LandingPageAuthenticated'
import LandingPageNotAuthenticated from './landing/LandingPageNotAuthenticated';

export interface BasePageProps {
    isAuthenticated: boolean;
}

function BasePage(props: BasePageProps): ReactNode {
    if (props.isAuthenticated) {
        return <LandingPageAuthenticated/>;
    }

    return <LandingPageNotAuthenticated/>
}

export default BasePage;
