![Dependabot](https://api.dependabot.com/badges/status?host=github&repo=joasimonson/CopaFilmes)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/64697a5f938a47cb9b90f00f76b50ccc)](https://app.codacy.com/gh/joasimonson/CopaFilmes?utm_source=github.com&utm_medium=referral&utm_content=joasimonson/CopaFilmes&utm_campaign=Badge_Grade_Settings)
[![Board Status](https://dev.azure.com/gitjoasimonson/c628e767-f76a-41bc-8a7a-25ab051c5730/a03e55ba-45d9-47a7-8145-af66c265c597/_apis/work/boardbadge/8c6ef803-c79c-4279-9108-138c05963fb1)](https://dev.azure.com/gitjoasimonson/c628e767-f76a-41bc-8a7a-25ab051c5730/_boards/board/t/a03e55ba-45d9-47a7-8145-af66c265c597/Microsoft.RequirementCategory)

<h1 align="center">
    <p>Copa de filmes</p>
</h1>

<p align="center">
    <a href="#-tecnologias">Tecnologias</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
    <a href="#-projeto">Projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
    <a href="#-back">Back</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
    <a href="#-front">Front</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
    <a href="#memo-licença">Licença</a>
</p>

## 🛠 Tecnologias

- [ASP.Net Core](https://docs.microsoft.com/pt-br/aspnet/core/)
- [.NET Core](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/en/)
- [ReactJS](https://reactjs.org)
- [TypeScript](https://www.typescriptlang.org/)

## 💻 Projeto

A Copa do Mundo de Filmes é um projeto para disputa de campeonatos com os filmes escolhidos pelo usuário.
A disputa é baseada nas notas dos filmes que foram avaliados pelo público previamente.

## 🧠 Back

```shell
$ cd server
$ dotnet restore
$ dotnet build
$ dotnet test
$ dotnet run -p .\src\CopaFilmes.Api\CopaFilmes.Api.csproj
```

> ***Nota**: Caso a raiz URL gerada na API não seja: https://localhost:44313, é necessário fazer a alteração no arquivo \copafilmes\web\src\config.ts*

## 🔖 Front
- [Layout Web](https://www.figma.com/file/85XOrFgiB0nKqKZD8GSdWp/Copa-de-Filmes?node-id=6%3A66)

```shell
$ cd web
$ npm install
$ npm start
```

## :memo: Licença

Esse projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
