using AutoBogus;
using Bogus;
using System.Linq;

namespace CopaFilmes.Tests.Common.Builders
{
    internal static class UtilFaker
    {
        public static readonly IAutoFaker Faker;
        public static readonly Faker FakerHub;
        public static readonly AutoFaker<string> FakerString;

        static UtilFaker()
        {
            Faker = AutoFaker.Create();
            FakerHub = new Faker();
            FakerString = new AutoFaker<string>();
        }

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
