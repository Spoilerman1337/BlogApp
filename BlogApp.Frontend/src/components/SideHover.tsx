import React, { ReactNode } from 'react';

export interface HoverProps {
    eventHandler: React.MouseEventHandler<HTMLElement>;
    children: ReactNode | ReactNode[];
}

function Hover(props: HoverProps): ReactNode {
    return <div onMouseEnter={props.eventHandler}>{props.children}</div>;
}

export default Hover;
