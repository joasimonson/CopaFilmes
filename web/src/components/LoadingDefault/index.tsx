import { FC, ReactNode } from 'react';

import Spinner from 'react-bootstrap/Spinner';

import 'bootstrap/dist/css/bootstrap.min.css';

type LoadingDefaultProps = {
    loading: boolean;
    mensagem?: string;
    children?: ReactNode;
};

const LoadingDefault: FC<LoadingDefaultProps> = props => {
    const msg = props.mensagem || 'Carregando...';

    return (
        <>
            {!props.loading ? (
                props.children
            ) : (
                <div className='info'>
                    <Spinner animation='border' role='status' data-testid='component-loading'>
                        <span className='sr-only'>{msg}</span>
                    </Spinner>
                    <span data-testid='loading-msg'>{msg}</span>
                </div>
            )}
        </>
    );
};

export default LoadingDefault;
