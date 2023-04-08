using CopaFilmes.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string[] _schemasToInclude = { "public" };
        private readonly Table[] _tablesToIgnore = new Table[] { "__EFMigrationsHistory" };

        private readonly ApiContext _context;
        private readonly string _connectionString;

        private bool _initialized;

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
                await Reset().ConfigureAwait(false);
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
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            Table[] tablesToInclude = tables?.Select(t => new Table(t)).ToArray() ?? Array.Empty<Table>();
            
            var respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = _schemasToInclude,
                TablesToIgnore = _tablesToIgnore,
                TablesToInclude = tablesToInclude
            });
            await respawner.ResetAsync(conn);

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
