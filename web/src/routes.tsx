import { BrowserRouter, Route } from 'react-router-dom';

import Resultado from './pages/Resultado';
import SelecaoFilmes from './pages/SelecaoFilmes';

function Routes(): JSX.Element {
    return (
        <BrowserRouter>
            <Route path='/' exact component={SelecaoFilmes} />
            <Route path='/resultado' component={Resultado} />
        </BrowserRouter>
    );
}

export default Routes;
