import { mocked } from 'ts-jest/utils';

import * as FilmeService from '../../services/FilmeService';
import { renderAll, waitForElementToBeRemoved, fireEvent, within, waitFor } from '../../tests/custom-render';
import mocks from '../../tests/mocks';

import SelecaoFilmes from './index';

jest.mock('../../services/FilmeService');

describe('Testando renderização da seleção de filmes', () => {
    const { filmesResponse } = mocks;
    const componentLoading = 'component-loading';
    const componentListaFilmes = 'lista-filmes';
    const componentCardFilme = 'card-filme';
    const componentCardCheckbox = 'card-filme-checkbox';
    const componentTotalFilmesSelecionados = 'total-filmes-selecionados';
    const componentTotalFilmesMax = 'total-filmes-max';

    beforeEach(() => {
        mocked(FilmeService.obterFilmes).mockImplementation(() => Promise.resolve(filmesResponse));
    });

    afterEach(() => {
        mocked(FilmeService.obterFilmes).mockRestore();
    });

    test('Testando se a chamada da API está sendo feita corretamente', async () => {
        //Arrange
        const { getByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        //Assert
        expect(FilmeService.obterFilmes).toHaveBeenCalledTimes(1);
    });

    test('Validar exibição da lista de filmes', async () => {
        //Arrange
        const { getByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        //Assert
        expect(getByTestId(componentListaFilmes)).toBeInTheDocument();
    });

    test('Validar se a quantidade de filmes presente na lista é igual a que veio externa', async () => {
        //Arrange
        const { getByTestId, getAllByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        const listCardFilme = getAllByTestId(componentCardFilme);

        //Assert
        expect(listCardFilme).toHaveLength(filmesResponse.length);
    });

    test('Validar se a quantidade de filmes máxima permitida é menor ou igual a quantidade disponível', async () => {
        //Arrange
        const { getByTestId, getAllByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        const elListaCardFilme = getAllByTestId(componentCardFilme);
        const elTotalFilmesMax = getByTestId(componentTotalFilmesMax);
        const totalFilmesMax = parseInt(elTotalFilmesMax.textContent as string);

        //Assert
        expect(elTotalFilmesMax).toBeInTheDocument();
        expect(totalFilmesMax).toBeLessThanOrEqual(elListaCardFilme.length);
    });

    test('Validar se a quantidade selecionada é maior que a permitida', async () => {
        //Arrange
        jest.spyOn(global, 'alert').mockImplementation(() => jest.fn());

        const { getByTestId, getAllByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        const elListaCardFilme = getAllByTestId(componentCardFilme);
        const elTotalFilmesMax = getByTestId(componentTotalFilmesMax);
        const totalFilmesMaxMaiorQuePermitido = parseInt(elTotalFilmesMax.textContent as string) + 1;

        for (let i = 0; i < totalFilmesMaxMaiorQuePermitido; i++) {
            const filmeElement = elListaCardFilme[i];

            const checkBox = within(filmeElement).getByTestId(componentCardCheckbox);

            fireEvent.click(checkBox);

            await waitFor(() => getByTestId(componentTotalFilmesSelecionados));
        }

        //Assert
        expect(global.alert).toHaveBeenCalledWith('Atenção! O total máximo de filmes já foi selecionado.');
    });

    test('Validar seleção de filmes para disputa do campeonato', async () => {
        //Arrange
        const { getByTestId, getAllByTestId } = renderAll(<SelecaoFilmes />);

        //Act
        await waitForElementToBeRemoved(() => getByTestId(componentLoading));

        const elListaCardFilme = getAllByTestId(componentCardFilme);
        const elTotalFilmesSelecionados = getByTestId(componentTotalFilmesSelecionados);
        const elTotalFilmesMax = getByTestId(componentTotalFilmesMax);
        const totalFilmesMax = parseInt(elTotalFilmesMax.textContent as string);

        for (let i = 0; i < totalFilmesMax; i++) {
            const filmeElement = elListaCardFilme[i];

            const checkBox = within(filmeElement).getByTestId(componentCardCheckbox);

            fireEvent.click(checkBox);

            await waitFor(() => getByTestId(componentTotalFilmesSelecionados));
        }

        //Assert
        expect(elTotalFilmesMax).toBeInTheDocument();

        expect(elTotalFilmesSelecionados).toBeInTheDocument();
        expect(elTotalFilmesSelecionados).toHaveTextContent(elTotalFilmesMax.textContent as string);
    });
});
