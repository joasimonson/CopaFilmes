import config from 'react-global-configuration';

var loaded = false;

export function init(): void {
    const comumConfiguration = {
        usuario: "jo",
        senha: "81dc9bdb52d04dc20036dbd8313ed055",
        TotalFilmesCampeonato: 8,
    };

    const configuration = {
        development: {
            URL_API: 'https://localhost:5001/api/v1',
        },
        test: {
            URL_API: 'https://localhost:5001/api/v1',
        },
        production: {
            URL_API: 'https://copafilmes.herokuapp.com/api/v1',
        }
    };

    let configs = Object.assign({}, configuration[process.env.NODE_ENV], comumConfiguration);

    config.set(configs);

    loaded = true;
}

export function getConfig(key: string): string {
    if (!loaded) {
        init();
    }

    return config.get(key);
}

export function getConfigDynamic<T>(key: string): T {
    if (!loaded) {
        init();
    }

    return config.get(key) as T;
}