namespace CopaFilmes.Api.Extensions
{
    public static class NumberExtensions
    {
        public static bool EhPar(this int numero)
        {
            return numero % 2 == 0;
        }
    }
}
