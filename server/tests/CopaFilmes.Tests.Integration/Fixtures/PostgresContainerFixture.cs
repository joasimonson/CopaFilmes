using CopaFilmes.Tests.Common.Util;
using Docker.DotNet;
using Docker.DotNet.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class PostgresContainerFixture : IDisposable
    {
        private const string _container = "copafilmes_postgres";
        private const string _image = "postgres";
        private readonly DockerClient _client;

        public PostgresContainerFixture()
        {
            _client = new DockerClientConfiguration().CreateClient();

            Inicializar().GetAwaiter().GetResult();
        }

        public async Task Inicializar()
        {
            var filteredContainers = await _client.Containers.ListContainersAsync(new()
            {
                All = true,
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    { "name", new Dictionary<string, bool> { { $"^/{_container}$", true} } }
                }
            });

            if (filteredContainers is null || filteredContainers.Count == 0)
            {
                using var conn = new NpgsqlConnection(ConfigManagerIntegration.TestConnectionString);
                var port = conn.Port.ToString();
                var user = conn.UserName;
                var password = conn.GetPropertyValue<string>("Password");
                await _client.Containers.CreateContainerAsync(new()
                {
                    Image = _image,
                    Name = _container,
                    HostConfig = new()
                    {
                        PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            { port, new List<PortBinding> { new() { HostPort = port } } }
                        }
                    },
                    ExposedPorts = new Dictionary<string, EmptyStruct>()
                    {
                        { port, new() }
                    },
                    Env = new string[]
                    {
                        $"POSTGRES_USER={user}",
                        $"POSTGRES_PASSWORD={password}"
                    }
                });
                await _client.Containers.StartContainerAsync(_container, new());

                await WaitForContainerStartsAsync(_container);
            }
            else
            {
                var container = filteredContainers.FirstOrDefault();
                switch (container.State)
                {
                    case "exited":
                        await _client.Containers.StartContainerAsync(_container, new());
                        break;
                    case "running":
                        break;
                    default:
                        throw new NotImplementedException("Status of container not mapped!");
                }
            }
        }

        private async Task WaitForContainerStartsAsync(string container)
        {
            var wait = 0;
            var maxWait = 10;
            IList<ContainerListResponse> containers;
            do
            {
                if (wait > maxWait)
                {
                    throw new TimeoutException("Time for wait container starts exceeded!");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
                containers = await _client.Containers.ListContainersAsync(new()
                {
                    All = true,
                    Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    { "name", new Dictionary<string, bool> { { $"^/{container}$", true} } },
                    { "status", new Dictionary<string, bool> { { "running", true} } }
                }
                });
                wait++;
            } while (containers is null || containers.Count == 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => _client.Dispose();
    }
}