import React, { FC, ReactElement } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import styles from './styles/App.module.scss';

const App: FC = (): ReactElement => {
    return (
        <div className={styles.app}>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<h1>Landing Page</h1>} />
                </Routes>
            </BrowserRouter>
        </div>
    );
};

export default App;
