import React from 'react';

import { Filme } from '../../../types/model';

import './styles.css';

type CardFilmeProps = {
    filme: Filme;
    handleSelecao: (event: React.ChangeEvent<HTMLInputElement>) => void;
};

const CardFilme: React.FC<CardFilmeProps> = props => {
    const { filme, handleSelecao } = props;

    return (
        <div className='card-filme' data-testid='card-filme'>
            <div className='selection'>
                <input
                    type='checkbox'
                    className='check'
                    onChange={handleSelecao}
                    id={filme.id}
                    data-testid='card-filme-checkbox'
                />
            </div>
            <div className='info-card'>
                <span className='titulo' data-testid='card-filme-titulo'>
                    {filme.titulo}
                </span>
                <span className='ano' data-testid='card-filme-ano'>
                    {filme.ano}
                </span>
            </div>
        </div>
    );
};

export default CardFilme;
