import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { getConfig } from '../config';

const api = axios.create({
    baseURL: getConfig('URL_API')
});

export const getAuth = async (url: string): Promise<AxiosResponse> => {
    async function get(config: AxiosRequestConfig) {
        return api.get(url, config);
    }

    return enviarComRetentativa(get);
};

export const postAuth = async <T>(url: string, data: T): Promise<AxiosResponse> => {
    async function post(config: AxiosRequestConfig) {
        return api.post(url, data, config);
    }

    return enviarComRetentativa(post);
};

const enviarComRetentativa = async (
    execute: (config: AxiosRequestConfig) => Promise<AxiosResponse>
): Promise<AxiosResponse> => {
    let config = await getConfigRequest();
    for (let i = 0; i < 2; i++) {
        try {
            return await execute(config);
        } catch (error) {
            console.error(error);
            config = await getConfigRequest(true);
        }
    }

    return {
        data: 'request failed'
    } as AxiosResponse;
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
