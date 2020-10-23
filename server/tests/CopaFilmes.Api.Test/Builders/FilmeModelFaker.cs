using AutoBogus;
using CopaFilmes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.Api.Test.Builders
{
    public class FilmeModelFaker : AutoFaker<FilmeModel>
    {
        public FilmeModelFaker()
        {
            RuleFor(c => c.Id, r => r.Random.Guid().ToString())
            .RuleFor(c => c.Nota, r => UtilFaker.Nota())
            .RuleFor(c => c.Ano, r => r.Random.Int(DateTime.MinValue.Year, DateTime.Today.Year));
        }

        public static FilmeModelFaker Novo()
        {
            return new FilmeModelFaker();
        }

        public FilmeModelFaker ComNota(decimal nota)
        {
            RuleFor(c => c.Nota, r => nota);
            return this;
        }

        public FilmeModelFaker ComTitulo(string titulo)
        {
            RuleFor(c => c.Titulo, r => titulo);
            return this;
        }

        public FilmeModelFaker Exclude(FilmeModel filmeModel)
        {
            if (filmeModel is null)
            {
                return this;
            }

            return ExcludeNota(filmeModel.Nota).ExcludeTitulo(filmeModel.Titulo);
        }

        public FilmeModelFaker GenerateExclude(List<FilmeModel> filmes)
        {
            decimal[] notas = filmes.Select(f => f.Nota).ToArray();
            string[] titulos = filmes.Select(f => f.Titulo).ToArray();

            return ExcludeNota(notas).ExcludeTitulo(titulos);
        }

        public FilmeModelFaker ExcludeNota(params decimal[] nota)
        {
            RuleFor(c => c.Nota, r => UtilFaker.Nota(nota));
            return this;
        }

        public FilmeModelFaker ExcludeTitulo(params string[] titulo)
        {
            RuleFor(c => c.Titulo, r => UtilFaker.Nome(titulo));
            return this;
        }

        public List<FilmeModel> GenerateRandomList(int count = 8)
        {
            return Generate(count);
        }

        public List<FilmeModel> GenerateDifferentList(int count = 8)
        {
            var list = new List<FilmeModel>();

            for (int i = 0; i < count; i++)
            {
                var item = GenerateExclude(list).Generate();

                list.Add(item);
            }

            return list.OrderByDescending(f => f.Nota).ThenBy(f => f.Titulo).ToList();
        }
    }
}
