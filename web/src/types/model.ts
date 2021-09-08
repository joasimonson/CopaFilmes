export interface Filme {
    id: string;
    titulo: string;
    ano: number;
    nota: number;
}

export interface FilmePosicao extends Filme {
    posicao: number;
}

export interface AuthData {
    autenticado: boolean;
    criacao: Date;
    expiracao: Date;
    token: string;
    usuario: string;
    mensagem: string;
}
