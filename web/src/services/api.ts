import axios from 'axios';
import { getConfig } from '../config';

const api = axios.create({
    baseURL: getConfig("URL_API"),
});

export const getAuth = async (url: string) => {
    const config = await getConfigRequest();

    return await api.get(url, config);
}

export const postAuth = async (url: string, data: any) => {
    const config = await getConfigRequest();

    return await api.post(url, data, config);
}

const getConfigRequest = async () => {
    let token = sessionStorage.getItem("token");

    if (!token || token === "undefined") {
        const usuario = getConfig('usuario');
        const senha = getConfig('senha');

        const params = {
            usuario,
            senha
        };

        const authResponse = await api.post('login', params);

        const authData = authResponse?.data;

        sessionStorage.setItem("token", authData?.token);

        token = authData?.token;
    }

    const config = {
        headers: {
            "Authorization" : `Bearer ${token}`
        }
    };

    return config;
}

export default api;