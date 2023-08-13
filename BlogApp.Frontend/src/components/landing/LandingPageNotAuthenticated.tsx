import React, { ReactNode, useState } from 'react';
import styles from '../../styles/LandingPageNotAuthenticated.module.scss';
import Offcanvas from 'react-bootstrap/Offcanvas';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import SideHover from '../SideHover';
import { signinRedirect } from '../../auth/userService';

function LandingPageNotAuthenticated(): ReactNode {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <>
            <SideHover eventHandler={handleShow}>
                <div className={styles.hover}></div>
            </SideHover>
            <Offcanvas show={show} onHide={handleClose} placement={'end'}>
                <Offcanvas.Header closeButton>
                    <Offcanvas.Title>Authentication</Offcanvas.Title>
                </Offcanvas.Header>
                <Offcanvas.Body>
                    <Container>
                        <Row>
                            Sign up or sign in to communicate with those, who
                            share your interest in things you like
                        </Row>
                        <Row>
                            <div className="d-grid pt-3">
                                <Button
                                    variant="outline-primary"
                                    onClick={() => signinRedirect()}
                                >
                                    Sign-in
                                </Button>
                            </div>
                        </Row>
                    </Container>
                </Offcanvas.Body>
            </Offcanvas>
        </>
    );
}

export default LandingPageNotAuthenticated;
