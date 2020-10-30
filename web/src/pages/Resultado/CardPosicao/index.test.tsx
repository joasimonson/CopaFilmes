import React from 'react';
import { render } from '@testing-library/react';

import CardPosicao from './index';

import { FilmePosicao } from '../../../types/model';

describe('Testando renderização do card de posição da disputa do campeonato', () => {
    const componentPosicao = 'card-posicao-numposicao';
    const componentTitulo = 'card-posicao-titulo';
    const componentAno = 'card-posicao-ano';

    test('Renderização padrão', () => {
        //Arrange
        const filmePosicao: FilmePosicao = {
            posicao: 1,
            id: "012345",
            titulo: "Título do filme",
            nota: 8,
            ano: 2010
        };

        //Act
        const { getByTestId } = render(<CardPosicao filme={filmePosicao} />);

        const elPosicao = getByTestId(componentPosicao);
        const elTitulo = getByTestId(componentTitulo);
        const elAno = getByTestId(componentAno);

        //Assert
        expect(elPosicao).toBeInTheDocument();
        expect(elPosicao).toHaveTextContent(filmePosicao.posicao.toString() + 'º');

        expect(elTitulo).toBeInTheDocument();
        expect(elTitulo).toHaveTextContent(filmePosicao.titulo);

        expect(elAno).toBeInTheDocument();
        expect(elAno).toHaveTextContent(filmePosicao.ano.toString());
    });
});