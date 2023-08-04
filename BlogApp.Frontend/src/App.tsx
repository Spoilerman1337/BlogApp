import React, { FC, ReactElement } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

const App: FC<object> = (): ReactElement => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<h1>Home</h1>} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
