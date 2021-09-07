import axios, { AxiosResponse } from 'axios';
import { getConfig } from '../config';

const api = axios.create({
    baseURL: getConfig('URL_API')
});

export const getAuth = async (url: string): Promise<AxiosResponse> => {
    async function get(config: any) {
        return api.get(url, config);
    }

    return enviarComRetentativa(get);
};

export const postAuth = async (url: string, data: any): Promise<AxiosResponse> => {
    async function post(config: any) {
        return api.post(url, data, config);
    }

    return enviarComRetentativa(post);
};

const enviarComRetentativa = async (execute: any): Promise<AxiosResponse> => {
    let config = await getConfigRequest();

    let response;

    for (let i = 0; i < 2; i++) {
        try {
            response = await execute(config);
        } catch (error) {
            console.error(error);
            config = await getConfigRequest(true);
        }
    }

    return response;
};

const getConfigRequest = async (autenticar = false) => {
    let token = sessionStorage.getItem('token');

    if (autenticar || !token || token === 'undefined') {
        const usuario = getConfig('usuario');
        const senha = getConfig('senha');

        const params = {
            usuario,
            senha
        };

        const authResponse = await api.post('login', params);

        const authData = authResponse?.data;

        sessionStorage.setItem('token', authData?.token);

        token = authData?.token;
    }

    return {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
};

export default api;
