﻿using AutoBogus;
using Bogus;
using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Filme;
using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Test.Builders;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Api.Test.Dominio
{
    public class FilmeDominioTest
    {
        private readonly HttpTest _httpTest;
        private readonly IFilmeDominio _filmeDominio;
        private readonly IEnumerable<FilmeModel> _filmes;

        public FilmeDominioTest()
        {
            _httpTest = new HttpTest();

            _filmes = FilmeModelFaker.Novo().Generate(8);

            _filmeDominio = new FilmeDominio();
        }

        [Fact]
        public async Task ObterFilmesAsync_DeveChamarEndpointFilmes()
        {
            //Arrange
            _httpTest.RespondWithJson(_filmes);

            //Act
            await _filmeDominio.ObterFilmesAsync();

            //Assert
            _httpTest.ShouldHaveCalled(Parametros.URI_FILMES)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        public async Task ObterFilmesAsync_DeveRetornarFalhaTimeout()
        {
            //Arrange
            _httpTest.SimulateTimeout();

            //Act
            Func<Task> act = async () => await _filmeDominio.ObterFilmesAsync();

            //Assert
            await act.Should().ThrowAsync<FlurlHttpTimeoutException>();
        }

        [Fact]
        public async Task ObterFilmesAsync_DeveRetornarFalhaAoConverterRetornoApi()
        {
            //Arrange
            var retornoIncorreto = new { x = 1, y = 2 };
            _httpTest.RespondWithJson(retornoIncorreto);

            //Act
            Func<Task> act = async () => await _filmeDominio.ObterFilmesAsync();

            //Assert
            await act.Should().ThrowAsync<FlurlHttpException>()
                .WithMessage("*Response could not be deserialized to JSON*");
        }

        [Fact]
        public async Task ObterFilmesAsync_DeveRetornarFilmes()
        {
            //Arrange
            _httpTest.RespondWithJson(_filmes);

            //Act
            var filmes = await _filmeDominio.ObterFilmesAsync();

            //Assert
            filmes.Should().BeEquivalentTo(_filmes);
        }
    }
}
