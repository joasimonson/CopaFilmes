import { render } from '@testing-library/react';

import PageHeader from './index';

describe('Testando renderização de Header', () => {
    const componentTitulo = 'page-header-titulo';
    const componentDescricao = 'page-header-descricao';

    test('Renderização padrão de header', () => {
        //Arrange
        const titulo = 'Título do header';
        const descricao = 'Descrição do header';

        //Act
        const { getByTestId } = render(<PageHeader titulo={titulo} descricao={descricao} />);
        const elTitulo = getByTestId(componentTitulo);
        const elDescricao = getByTestId(componentDescricao);

        //Assert
        expect(elTitulo).toBeInTheDocument();
        expect(elTitulo).toHaveTextContent(titulo);

        expect(elDescricao).toBeInTheDocument();
        expect(elDescricao).toHaveTextContent(descricao);
    });

    test('Renderização de header sem descrição', () => {
        //Arrange
        const titulo = 'Título do header';
        const descricao = '';

        //Act
        const { queryByTestId } = render(<PageHeader titulo={titulo} descricao={descricao} />);
        const elDescricao = queryByTestId(descricao);

        //Assert
        expect(elDescricao).not.toBeInTheDocument();
    });
});
