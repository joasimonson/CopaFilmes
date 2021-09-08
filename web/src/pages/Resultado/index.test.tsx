import { when, resetAllWhenMocks } from 'jest-when';

import Resultado from './index';

import { renderAll } from '../../tests/custom-render';
import { FilmesStore } from '../../stores/store';
import mocks from '../../tests/mocks';

describe('Testando renderização do resultado do campeonato', () => {
    const { filmesPosicaoResponse } = mocks;
    const componentCardPosicao = 'card-posicao';

    const pullstateUseStateMock = jest.fn();

    beforeEach(() => {
        jest.spyOn(FilmesStore, 'useState').mockImplementation((args: unknown) => {
            const safeArgs = args as string;
            const params = safeArgs.toString().split('.');
            let type = params[params.length - 1];

            // Tratamento para execução com report coverage, onde são enviados mais parâmetros para análise
            const index = type.indexOf(';');
            if (index !== -1) {
                type = type.substring(0, index);
            }

            return pullstateUseStateMock(type);
        });
    });

    afterEach(() => {
        jest.restoreAllMocks();
        resetAllWhenMocks();
    });

    test('Validar se finalistas estão sendo renderizados na quantidade correta baseado no state', async () => {
        //Arrange
        when(pullstateUseStateMock)
            .calledWith('disputandoCampeonato')
            .mockReturnValue(false)
            .calledWith('filmesResultado')
            .mockReturnValue(filmesPosicaoResponse);

        //Act
        const { getAllByTestId } = renderAll(<Resultado />);

        const elListaResultado = getAllByTestId(componentCardPosicao);

        //Assert
        expect(elListaResultado).toHaveLength(filmesPosicaoResponse.length);
    });

    test('Validar se finalistas estão sendo renderizados na quantidade correta baseado no localStorage', async () => {
        //Arrange
        const localStorageGetItemMock = jest.fn();

        jest.spyOn(global.localStorage.__proto__, 'getItem').mockImplementation((args: unknown) => {
            return localStorageGetItemMock(args);
        });

        when(pullstateUseStateMock)
            .calledWith('disputandoCampeonato')
            .mockReturnValue(false)
            .calledWith('filmesResultado')
            .mockReturnValue(undefined);

        when(localStorageGetItemMock)
            .calledWith('filmesResultado')
            .mockReturnValue(JSON.stringify(filmesPosicaoResponse));

        //Act
        const { getAllByTestId } = renderAll(<Resultado />);

        const elListaResultado = getAllByTestId(componentCardPosicao);

        //Assert
        expect(localStorage.getItem).toHaveBeenCalled();
        expect(elListaResultado).toHaveLength(filmesPosicaoResponse.length);
    });

    test('Validar se conteúdo será renderizado corretamente após o loading', async () => {
        //Arrange
        when(pullstateUseStateMock)
            .calledWith('filmesResultado')
            .mockReturnValueOnce(undefined)
            .mockReturnValue(filmesPosicaoResponse)
            .calledWith('disputandoCampeonato')
            .mockReturnValueOnce(true)
            .mockReturnValue(false);

        const { rerender, getAllByTestId } = renderAll(<Resultado />);

        //Act
        rerender(<Resultado />);

        const elListaResultado = getAllByTestId(componentCardPosicao);

        //Assert
        expect(elListaResultado).toHaveLength(filmesPosicaoResponse.length);
    });
});
