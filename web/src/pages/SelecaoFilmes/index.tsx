import { ChangeEvent, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import PageHeader from '../../components/PageHeader';
import CardFilme from './CardFilme';
import LoadingDefault from '../../components/LoadingDefault';

import { obterFilmes, gerarDisputaCampeonato } from '../../services/FilmeService';
import { getConfigDynamic } from '../../config';
import { FilmesStore } from '../../stores/store';
import { Filme } from '../../types/model';

import './styles.css';

function SelecaoFilmes(): JSX.Element {
    const [loading, setLoading] = useState<boolean>(true);
    const [filmes, setFilmes] = useState<Filme[]>([]);
    const [filmesSelecionados, setFilmesSelecionados] = useState<Filme[]>([]);
    const [totalFilmesCampeonato, setTotalFilmesCampeonato] = useState<number>(0);
    const [totalFilmesSelecionados, setTotalFilmesSelecionados] = useState<number>(0);

    useEffect(() => {
        (async function obterFilmesAsync() {
            const filmesResponse = await obterFilmes();

            setFilmes(filmesResponse);
            setLoading(false);
        })();

        const totalFilmes = getConfigDynamic<number>('TotalFilmesCampeonato');
        setTotalFilmesCampeonato(totalFilmes);
    }, []);

    function handleFilmeSelecionado(event: ChangeEvent<HTMLInputElement>) {
        if (totalFilmesSelecionados >= totalFilmesCampeonato && event.target.checked) {
            alert('Atenção! O total máximo de filmes já foi selecionado.');
            event.target.checked = false;
            return;
        }

        const newFilmesSelecionados = filmesSelecionados.filter(item => item.id !== event.target.id);

        const filme = filmes.find(item => item.id === event.target.id);

        if (event.target.checked && filme) {
            newFilmesSelecionados.push(filme);
        }

        setFilmesSelecionados(newFilmesSelecionados);
        setTotalFilmesSelecionados(newFilmesSelecionados.length);
    }

    async function handleGerarDisputaCampeonato(event: React.FormEvent<HTMLInputElement>) {
        if (totalFilmesSelecionados !== totalFilmesCampeonato) {
            alert('Atenção! Selecione a quantidade mínima de filmes.');
            event.preventDefault();
            return;
        }

        FilmesStore.update(s => {
            s.disputandoCampeonato = true;
        });

        const filmesResponse = await gerarDisputaCampeonato(filmesSelecionados);

        localStorage.setItem('filmesResultado', JSON.stringify(filmesResponse));

        FilmesStore.update(f => {
            f.filmesResultado = filmesResponse;
        });
        FilmesStore.update(s => {
            s.disputandoCampeonato = false;
        });
    }

    return (
        <div id='page-selecao-filmes' className='container-page'>
            <PageHeader
                titulo='Fase de Seleção'
                descricao='Selecione 8 filmes que você deseja que entrem na competição e depois pressione o botão Gerar Meu Campeonato para prosseguir.'
            />
            <div className='middle'>
                <div className='info-counter'>
                    <span>
                        Selecionados <br />
                        <span data-testid='total-filmes-selecionados'>{totalFilmesSelecionados}</span> de{' '}
                        <span data-testid='total-filmes-max'>{totalFilmesCampeonato}</span> filmes
                    </span>
                </div>

                <Link to='/resultado' className='btn-default' onClick={handleGerarDisputaCampeonato}>
                    GERAR MEU CAMPEONATO
                </Link>
            </div>
            <main>
                <LoadingDefault loading={loading}>
                    {filmes.length === 0 ? (
                        <span className='info' data-testid='info-indisponivel'>
                            Ops... Parece que os filmes não estão disponíveis.
                        </span>
                    ) : (
                        <div className='lista-filmes' data-testid='lista-filmes'>
                            {filmes.map(item => (
                                <CardFilme key={item.id} filme={item} handleSelecao={handleFilmeSelecionado} />
                            ))}
                        </div>
                    )}
                </LoadingDefault>
            </main>
        </div>
    );
}

export default SelecaoFilmes;
