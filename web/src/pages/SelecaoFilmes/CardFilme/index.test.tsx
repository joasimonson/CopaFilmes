import { render } from '@testing-library/react';

import { Filme } from '../../../types/model';

import CardFilme from './index';

describe('Testando renderização do card de filme', () => {
    const componentCheckbox = 'card-filme-checkbox';
    const componentTitulo = 'card-filme-titulo';
    const componentAno = 'card-filme-ano';

    test('Renderização padrão', () => {
        //Arrange
        const filme: Filme = {
            id: '012345',
            titulo: 'Título do filme',
            nota: 8,
            ano: 2010
        };

        const mockHandleSelecao = jest.fn();

        //Act
        const { getByTestId } = render(<CardFilme filme={filme} handleSelecao={mockHandleSelecao} />);

        const elCheckbox = getByTestId(componentCheckbox);
        const elTitulo = getByTestId(componentTitulo);
        const elAno = getByTestId(componentAno);

        //Assert
        expect(elCheckbox).toBeInTheDocument();
        expect(elCheckbox).toHaveAttribute('id', filme.id);

        expect(elTitulo).toBeInTheDocument();
        expect(elTitulo).toHaveTextContent(filme.titulo);

        expect(elAno).toBeInTheDocument();
        expect(elAno).toHaveTextContent(filme.ano.toString());
    });
});
