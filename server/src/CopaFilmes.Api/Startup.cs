using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Repositorios;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.StartupConfigure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CopaFilmes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SegurancaStartup.ConfigureJwtToken(services, Configuration);
            SwaggerStartup.Configurar(services);

            services.AddHttpClient();

            services.AddSingleton<Endpoints>();

            services.AddScoped<IRecursos, Recursos>();

            services.AddScoped<ILoginServico, LoginServico>();

            services.AddScoped<IFilmeRepositorio, FilmeRepositorio>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(opt => {
                opt.RoutePrefix = "swagger";
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Copa Filmes");
            });

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
