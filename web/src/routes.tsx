import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom';
import SelecaoFilmes from './pages/SelecaoFilmes';
import Resultado from './pages/Resultado';

function Routes() {
    return (
        <BrowserRouter>
            <Route path="/" exact component={SelecaoFilmes} />
            <Route path="/resultado" component={Resultado} />
        </BrowserRouter>
    );
}

export default Routes;