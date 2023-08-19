import React, { ReactNode } from 'react';
import Image from 'react-bootstrap/Image';
import FormText from 'react-bootstrap/FormText';

export interface ProfileNavProps {
    avatar: string;
    userName: string;
}

function ProfileNav({ avatar, userName }: ProfileNavProps): ReactNode {
    return (
        <>
            <Image src={avatar} roundedCircle />
            <FormText>{userName}</FormText>
        </>
    );
}

export default ProfileNav;
