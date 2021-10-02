using AutoBogus;
using Bogus;
using System.Linq;

namespace CopaFilmes.Tests.Common.Builders
{
    internal static class UtilFaker
    {
        public static readonly IAutoFaker Faker = AutoFaker.Create();
        public static readonly Faker FakerHub = new();
        public static readonly AutoFaker<string> FakerString = new();

        public static decimal Nota() => decimal.Round(FakerHub.Random.Decimal(0, 10), 2);

        public static decimal Nota(params decimal[] exclude)
        {
            decimal nota;

            do
            {
                nota = Nota();
            } while (exclude.Contains(nota));

            return nota;
        }

        public static string Nome(params string[] exclude)
        {
            string nome;

            do
            {
                nome = FakerHub.Name.FullName();
            } while (exclude.Contains(nome));

            return nome;
        }
    }
}
