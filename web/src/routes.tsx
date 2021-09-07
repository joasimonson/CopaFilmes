import { BrowserRouter, Route } from 'react-router-dom';
import SelecaoFilmes from './pages/SelecaoFilmes';
import Resultado from './pages/Resultado';

function Routes(): JSX.Element {
    return (
        <BrowserRouter>
            <Route path='/' exact component={SelecaoFilmes} />
            <Route path='/resultado' component={Resultado} />
        </BrowserRouter>
    );
}

export default Routes;
