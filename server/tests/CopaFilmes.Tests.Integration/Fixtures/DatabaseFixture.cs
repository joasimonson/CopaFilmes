using CopaFilmes.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using System;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string[] SchemasToInclude = new[] { "public" };
        private readonly string[] TablesToIgnore = new[] { "__EFMigrationsHistory" };

        private readonly ApiContext _context;
        private readonly string _connectionString;

        private static bool _initialized;

        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{EnvironmentsExtensions.Test}.json")
                .Build();

            _connectionString = configuration.GetConnectionString("TestConnection");
            _context = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseNpgsql(_connectionString).Options);

            SetupDatabase().GetAwaiter().GetResult();
        }

        private async Task SetupDatabase()
        {
            if (_initialized)
            {
                await Reset();
            }
            else
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.MigrateAsync();
                _initialized = true;
            }
        }

        public ApiContext GetContext() => _context;

        public async Task Reset(string[] tablesToInclude = null)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var checkpoint = new Checkpoint()
            {
                SchemasToInclude = SchemasToInclude,
                DbAdapter = DbAdapter.Postgres
            };

            if (tablesToInclude is null)
            {
                checkpoint.TablesToIgnore = TablesToIgnore;
            }
            else
            {
                checkpoint.TablesToInclude = tablesToInclude;
            }

            await checkpoint.Reset(conn);
            await conn.CloseAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
