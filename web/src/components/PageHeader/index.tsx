import React from 'react';

import './styles.css';

interface PageHeaderProps {
  titulo: string;
  descricao?: string;
}

const PageHeader: React.FC<PageHeaderProps> = props => {
  return (
    <header className='page-header'>
      <div className='page-header-top'>
        <h4>CAMPEONATO DE FILMES</h4>
        <h1 data-testid='page-header-titulo'>{props.titulo}</h1>
      </div>
      <hr />
      <div className='page-header-content'>
        {props.descricao && <p data-testid='page-header-descricao'>{props.descricao}</p>}
      </div>
    </header>
  );
};

export default PageHeader;
