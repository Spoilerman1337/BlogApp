import React, { FC, ReactElement } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.scss';

const App: FC = (): ReactElement => {
    return (
        <div className="app">
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<h1>Landing Page</h1>} />
                </Routes>
            </BrowserRouter>
        </div>
    );
};

export default App;
