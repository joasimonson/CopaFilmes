import { useEffect, useState } from 'react';

import { Link } from 'react-router-dom';

import LoadingDefault from '../../components/LoadingDefault';
import PageHeader from '../../components/PageHeader';
import { FilmesStore } from '../../stores/store';
import { FilmePosicao } from '../../types/model';

import CardPosicao from './CardPosicao';

import './styles.css';

function Resultado(): JSX.Element {
  const disputandoCampeonato = FilmesStore.useState(s => s.disputandoCampeonato);
  const [filmes, setFilmes] = useState<FilmePosicao[]>([]);
  const filmesResultado = FilmesStore.useState(s => s.filmesResultado);

  useEffect(() => {
    if (disputandoCampeonato) {
      return;
    }

    let listaFilmes = filmesResultado;
    if (!listaFilmes || listaFilmes.length === 0) {
      const json = localStorage.getItem('filmesResultado') || '';

      if (!json) {
        return;
      }

      const obj: FilmePosicao[] = JSON.parse(json);
      listaFilmes = obj;
    }
    setFilmes(listaFilmes);
  }, [disputandoCampeonato, filmesResultado]);

  return (
    <div id='page-resultado' className='container-page'>
      <PageHeader
        titulo='Resultado final'
        descricao='Veja o resultado final do Campeonato de filmes de forma simples e rápida.'
      />
      <main>
        <LoadingDefault loading={disputandoCampeonato}>
          {filmes.length === 0 ? (
            <span className='info'>Nos desculpe, algo deu errado.</span>
          ) : (
            <div className='lista-resultado'>
              {filmes?.map(item => (
                <CardPosicao key={item.id} filme={item} />
              ))}
            </div>
          )}
        </LoadingDefault>
      </main>
      <footer>
        <Link to='/' className='btn-default'>
          NOVO CAMPEONATO
        </Link>
      </footer>
    </div>
  );
}

export default Resultado;
