import React, { ReactNode } from 'react';
import HeaderNavbar from "../HeaderNavbar";
import Container from "react-bootstrap/Container";
import styles from '../../styles/LandingPageAuthenticated.module.scss'

function LandingPageAuthenticated(): ReactNode {
    return <>
        <HeaderNavbar />
        <Container>
            <div className={styles.bodyCenter}>
                Hello World!
            </div>
        </Container>
    </>;
}

export default LandingPageAuthenticated;
