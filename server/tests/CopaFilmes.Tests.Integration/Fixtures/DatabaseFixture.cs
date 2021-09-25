using CopaFilmes.Api.Contexts;
using Microsoft.EntityFrameworkCore;
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
            _connectionString = ConfigManagerIntegration.TestConnectionString;
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

        public async Task Reset(string[] tables = null)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var checkpoint = new Checkpoint()
            {
                SchemasToInclude = SchemasToInclude,
                DbAdapter = DbAdapter.Postgres
            };

            if (tables is null)
            {
                checkpoint.TablesToIgnore = TablesToIgnore;
            }
            else
            {
                checkpoint.TablesToInclude = tables;
            }

            await checkpoint.Reset(conn);
            await conn.CloseAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => _context.Dispose();
    }
}
