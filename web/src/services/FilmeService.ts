import { Filme } from '../types/model';

import { getAuth, postAuth } from './api';

export async function obterFilmes() {
    let filmes = [];

    try {
        const response = await getAuth('filme');

        filmes = response.data;
    } catch (error) {
        console.log(error);
    }

    return filmes;
}

export async function gerarDisputaCampeonato(filmes: Array<Filme>) {
    if (filmes.length === 0) {
        return [];
    }

    const params = filmes.map(item => {
        return {
            idFilme: item.id
        };
    });

    let resultadoDisputa = [];

    try {
        const response = await postAuth('campeonato', params);

        resultadoDisputa = response.data;
    } catch (error) {
        console.log(error);
    }

    return resultadoDisputa;
}
