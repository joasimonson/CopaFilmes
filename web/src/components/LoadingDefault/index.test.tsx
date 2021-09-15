import { render } from '@testing-library/react';

import LoadingDefault from './index';

describe('Testando renderização de Loading', () => {
    const componentLoading = 'component-loading';
    const componentMsg = 'loading-msg';

    test('Renderização padrão', () => {
        //Arrange
        const loading = true;

        //Act
        const { getByTestId } = render(<LoadingDefault loading={loading} />);
        const elLoading = getByTestId(componentLoading);
        const elMensagem = getByTestId(componentMsg);

        //Assert
        expect(elLoading).toBeInTheDocument();
        expect(elMensagem).toBeInTheDocument();
    });

    test('Renderização padrão com mensagem customizada', () => {
        //Arrange
        const loading = true;
        const mensagem = 'Loading custom';

        //Act
        const { getByTestId } = render(<LoadingDefault loading={loading} mensagem={mensagem} />);

        const elLoading = getByTestId(componentLoading);
        const elMensagem = getByTestId(componentMsg);

        //Assert
        expect(elLoading).toBeInTheDocument();

        expect(elMensagem).toBeInTheDocument();
        expect(elMensagem).toHaveTextContent(mensagem);
    });

    test('Renderização pós loading com elementos filhos', () => {
        //Arrange
        const loading = false;
        const dataTestId = 'loading-children';

        //Act
        const { getByTestId, queryByTestId } = render(
            <LoadingDefault loading={loading}>
                <span data-testid={dataTestId}>{dataTestId}</span>
            </LoadingDefault>
        );

        const elLoading = queryByTestId(componentLoading);
        const elChildren = getByTestId(dataTestId);

        //Assert
        expect(elLoading).not.toBeInTheDocument();

        expect(elChildren).toBeInTheDocument();
        expect(elChildren).toHaveTextContent(dataTestId);
    });
});
