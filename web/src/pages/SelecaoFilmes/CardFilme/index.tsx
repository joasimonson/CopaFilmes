import React from 'react';

import { Filme } from '../../../types/model';

import './styles.css';

type CardFilmeProps = {
    filme: Filme;
    handleSelecao: any
}

const CardFilme: React.FC<CardFilmeProps> = (props) => {
    const { filme, handleSelecao } = props;

    return (
        <div className="card-filme">
            <div className="selection">
                <input type="checkbox" className="check" onChange={handleSelecao} id={filme.id} />
            </div>
            <div className="info-card">
                <span className="titulo">{filme.titulo}</span>
                <span className="ano">{filme.ano}</span>
            </div>
        </div>
    );
}

export default CardFilme;