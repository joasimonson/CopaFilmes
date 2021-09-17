import { lazy, Suspense } from 'react';

import { BrowserRouter, Route } from 'react-router-dom';

import LoadingDefault from './components/LoadingDefault';

const Resultado = lazy(() => import('./pages/Resultado'));
const SelecaoFilmes = lazy(() => import('./pages/SelecaoFilmes'));

function Routes(): JSX.Element {
  return (
    <BrowserRouter>
      <Suspense fallback={<LoadingDefault loading={true}></LoadingDefault>}>
        <Route path='/' exact component={SelecaoFilmes} />
        <Route path='/resultado' component={Resultado} />
      </Suspense>
    </BrowserRouter>
  );
}

export default Routes;
