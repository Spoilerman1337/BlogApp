import React from 'react'
import ReactDOM from 'react-dom/client'
import App from '../App'

const root = document.getElementById('root');
if (root === null) throw new Error('Root container missing in index.html');

const rootRender = ReactDOM.createRoot(root);
const element = (
    <React.StrictMode>
        <App />
    </React.StrictMode>
);
rootRender.render(element);