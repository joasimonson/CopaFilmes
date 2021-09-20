using CopaFilmes.Api.Util;
using CopaFilmes.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class DatabaseStartup
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            var param = configuration.GetConnectionString("DefaultConnection");
            
            var connectionString = DatabaseCommon.ParseConnectionString(param);

            services.AddDbContext<ApiContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
