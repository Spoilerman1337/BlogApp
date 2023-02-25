import React, { FC, ReactElement } from "react";
import { BrowserRouter, Route, Router, Routes } from "react-router-dom";

const App: FC<{}> = (): ReactElement => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<h1>Home</h1>} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;