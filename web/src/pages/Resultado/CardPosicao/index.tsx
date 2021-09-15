import React from 'react';

import { FilmePosicao } from '../../../types/model';

import './styles.css';

type CardPosicaoProps = {
    filme: FilmePosicao;
};

const CardPosicao: React.FC<CardPosicaoProps> = props => {
    const { filme } = props;

    return (
        <div className='card-posicao' data-testid='card-posicao'>
            <div className='posicao'>
                <span data-testid='card-posicao-numposicao'>{filme.posicao}º</span>
            </div>
            <div className='info-card'>
                <span className='titulo' data-testid='card-posicao-titulo'>
                    {filme.titulo}
                </span>
                <span className='ano' data-testid='card-posicao-ano'>
                    {filme.ano}
                </span>
            </div>
        </div>
    );
};

export default CardPosicao;
