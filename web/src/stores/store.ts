import { Store } from 'pullstate';
import { FilmePosicao } from '../types/model';

export const FilmesStore = new Store({
    filmesResultado: new Array<FilmePosicao>(),
    disputandoCampeonato: false
});
