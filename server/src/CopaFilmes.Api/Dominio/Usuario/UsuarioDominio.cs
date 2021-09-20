using CopaFilmes.Api.Contexts;
using CopaFilmes.Api.Util;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Usuario
{
    internal class UsuarioDominio : IUsuarioDominio
    {
        private readonly ApiContext _context;

        public UsuarioDominio(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> CriarAsync(string usuario, string senha)
        {
            _context.Usuario.Add(new UsuarioEntity()
            {
                Usuario = usuario,
                Senha = SegurancaCommon.Criptografar(senha)
            });

            await _context.SaveChangesAsync();

            return true;
        }

        public bool Existe(string usuario, string senha)
        {
            return _context.Usuario.Any(u => u.Usuario == usuario && u.Senha == SegurancaCommon.Criptografar(senha));
        }
    }
}
