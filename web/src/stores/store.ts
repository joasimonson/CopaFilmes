import { Store } from 'pullstate';
import { FilmePosicao } from '../types/model';

export const FilmesStore = new Store({
    filmesResultado: [] as FilmePosicao[],
    disputandoCampeonato: false
});
