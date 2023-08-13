import React, { ReactElement } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import styles from './styles/App.module.scss';
import BasePage from './components/BasePage';

function App(): ReactElement {
    return (
        <div className={styles.app}>
            <BrowserRouter>
                <Routes>
                    <Route
                        path="/"
                        element={<BasePage isAuthenticated={false} />}
                    />
                </Routes>
            </BrowserRouter>
        </div>
    );
}

export default App;
