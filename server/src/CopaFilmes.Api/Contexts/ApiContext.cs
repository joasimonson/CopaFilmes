using CopaFilmes.Api.Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CopaFilmes.Api.Contexts
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name
                            && level == LogLevel.Information)
                        .AddConsole();
                 }));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
