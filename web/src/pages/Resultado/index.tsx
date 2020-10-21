import React from 'react';
import { Link } from 'react-router-dom';

import PageHeader from '../../components/PageHeader';
import LoadingDefault from '../../components/LoadingDefault';
import CardPosicao from './CardPosicao';

import { FilmesStore } from '../../stores/store';

import './styles.css';

function Resultado() {
    const filmesResultado = FilmesStore.useState(s => s.filmesResultado);
    const disputandoCampeonato = FilmesStore.useState(s => s.disputandoCampeonato);

    return (
        <div id="page-resultado" className="container-page">
            <PageHeader
                titulo="Resultado final"
                descricao="Veja o resultado final do Campeonato de filmes de forma simples e rápida."
            />
            <main>
                <LoadingDefault loading={disputandoCampeonato}>
                    {filmesResultado.length === 0 ?
                        (
                            <span className="info">
                                Nos desculpe, algo deu errado.
                            </span>
                        )
                        :
                        (
                            <div className="lista-resultado">
                                {filmesResultado?.map(item =>
                                    <CardPosicao filme={item} />
                                )}
                            </div>
                        )
                    }
                </LoadingDefault>    
            </main>
            <footer>
                <Link to="/" className="btn-default">NOVO CAMPEONATO</Link>
            </footer>
        </div>
    );
}

export default Resultado;