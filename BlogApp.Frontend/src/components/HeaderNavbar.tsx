import React, { ReactNode } from "react";
import Navbar from 'react-bootstrap/Navbar'
import Container from 'react-bootstrap/Container'

function HeaderNavbar(): ReactNode {
    return (
        <>
            <Navbar>
                <Container>
                    <Navbar.Brand href="/">
                        BlogApp
                    </Navbar.Brand>
                    <Navbar.Toggle />
                    <Navbar.Collapse className="justify-content-end">
                        <Navbar.Text>
                            Profile
                        </Navbar.Text>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    );
}

export default HeaderNavbar;