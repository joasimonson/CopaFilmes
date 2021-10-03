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
            const string connectionName = "DefaultConnection";
            
            var param = configuration.GetConnectionString(connectionName);
            var connectionString = DatabaseCommon.ParseConnectionString(param);
            
            services.AddHealthChecksUI()
                .AddPostgreSqlStorage(connectionString);
            
            services.AddHealthChecks()
                .AddNpgSql(connectionString)
                .AddDbContextCheck<ApiContext>();
            
            services.AddDbContext<ApiContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
