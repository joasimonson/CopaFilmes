[![Board Status](https://dev.azure.com/gitjoasimonson/c628e767-f76a-41bc-8a7a-25ab051c5730/a03e55ba-45d9-47a7-8145-af66c265c597/_apis/work/boardbadge/8c6ef803-c79c-4279-9108-138c05963fb1)](https://dev.azure.com/gitjoasimonson/c628e767-f76a-41bc-8a7a-25ab051c5730/_boards/board/t/a03e55ba-45d9-47a7-8145-af66c265c597/Microsoft.RequirementCategory)
# :pushpin: Copa do Mundo de Filmes
* [O que é o projeto](#about)
* [Como usar](#run)
    - [Back-end](#back)
    - [Front-end](#front)
* [Tecnologias utilizadas](#tech)

<h1 name="about">ℹ O que é o projeto</h1>

A Copa do Mundo de Filmes é um projeto para disputa de campeonatos com os filmes escolhidos pelo usuário.
A disputa é baseada nas notas dos filmes que foram avaliados pelo público previamente.

- Layout: https://www.figma.com/file/85XOrFgiB0nKqKZD8GSdWp/Copa-de-Filmes?node-id=6%3A66

<h1 name="run">:construction_worker: Como usar</h1>

> ### <a name="back">Back-end<a/>

> Clone o repositório
```shell
$ git clone https://github.com/joasimonson/CopaFilmes.git
```
> Na raiz do repositório vá até a pasta /server
```shell
$ cd server
```
> **Visual Studio**
> - Execute o arquivo CopaFilmes.sln;
> - Após abrir, clique com o botão direito na solution "Restore Nuget Packages";
> - Após finalizar a instalação dos pacotes, clique com o botão direito na solution "Build Solution";
> - Após a finalização do build, clique com o botão direito no projeto "CopaFilmes.Api.Test" e selecione "Run Tests";
> - Após todos os testes passarem, pressione F5 no teclado para executar o projeto.

> **VS Code**
```shell
$ dotnet restore
$ dotnet build
$ dotnet test
$ dotnet run -p .\src\CopaFilmes.Api\CopaFilmes.Api.csproj
```

>> ***Nota**: Caso a raiz URL gerada na API não seja: https://localhost:44313, é necessário fazer a alteração no arquivo \copafilmes\web\src\config.ts*

> ### <a name="front">Front-end<a/>

> Na raiz do repositório vá até a pasta /web
```shell
$ cd web
```
> Instale as dependências
```shell
$ npm install
# ou
$ yarn
```
> Execute
```shell
# In the folder each application
$ npm start
# ou
$ yarn start
```

<h1 name="tech">🛠 Tecnologias utilizadas</h1>

- ASP.Net Core
- DotNet Core
- ReactJS
- TypeScript
