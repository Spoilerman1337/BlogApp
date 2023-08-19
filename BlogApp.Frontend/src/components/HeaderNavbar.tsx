import React, { ReactNode } from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import ProfileNav from './ProfileNav';

function HeaderNavbar(): ReactNode {
    return (
        <>
            <Navbar>
                <Container>
                    <Navbar.Brand href="/">BlogApp</Navbar.Brand>
                    <Navbar.Toggle />
                    <Navbar.Collapse className="justify-content-end">
                        <Navbar.Text>
                            <ProfileNav
                                avatar={'/'}
                                userName={'Placeholder Username'}
                            />
                        </Navbar.Text>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    );
}

export default HeaderNavbar;
