import React from 'react';

import { FilmePosicao } from '../../../types/model';

import './styles.css';

type CardPosicaoProps = {
    filme: FilmePosicao;
}

const CardPosicao: React.FC<CardPosicaoProps> = (props) => {
    const { filme } = props;

    return (
        <div className="card-posicao">
            <div className="posicao">
                <span>{filme.posicao}º</span>
            </div>
            <div className="info-card">
                <span className="titulo">{filme.titulo}</span>
                <span className="ano">{filme.ano}</span>
            </div>
        </div>
    );
}

export default CardPosicao;