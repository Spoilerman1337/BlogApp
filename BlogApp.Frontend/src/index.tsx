import React, { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './styles/index.scss';

const root = document.getElementById('root');
if (root === null) throw new Error('Root container missing in index.html');

const rootRender = ReactDOM.createRoot(root);
const element = (
    <StrictMode>
        <App />
    </StrictMode>
);
rootRender.render(element);
