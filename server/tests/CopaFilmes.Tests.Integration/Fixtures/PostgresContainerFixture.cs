using Docker.DotNet;
using Docker.DotNet.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures;

public class PostgresContainerFixture : IDisposable
{
	private const string CONTAINER = "copafilmes_postgres";
	private const string IMAGE = "postgres";
	private const string TAGIMAGE = "latest";
	private const string FULLIMAGE = IMAGE + ":" + TAGIMAGE;

	private readonly DockerClient _client;

	public PostgresContainerFixture()
	{
		_client = new DockerClientConfiguration().CreateClient();

		Initialize().GetAwaiter().GetResult();
	}

	private async Task Initialize()
	{
		await PullImageIfNotExistsAsync(IMAGE, TAGIMAGE).ConfigureAwait(false);

		var filteredContainers = await _client.Containers.ListContainersAsync(new()
		{
			All = true,
			Filters = new Dictionary<string, IDictionary<string, bool>>
				{
					{ "name", new Dictionary<string, bool> { { $"^/{CONTAINER}$", true} } }
				}
		});

		if (filteredContainers is null || filteredContainers.Count == 0)
		{
			await CreatePostgresContainerAsync().ConfigureAwait(false);

			await _client.Containers.StartContainerAsync(CONTAINER, new());

			await WaitForContainerStartsAsync(CONTAINER).ConfigureAwait(false);
		}
		else
		{
			var container = filteredContainers.FirstOrDefault();
			switch (container.State)
			{
				case "exited":
					await _client.Containers.StartContainerAsync(CONTAINER, new());
					break;
				case "running":
					break;
				default:
					throw new NotImplementedException("Status of container not mapped!");
			}
		}
	}

	private async Task PullImageIfNotExistsAsync(string image, string tag) => await _client.Images.CreateImageAsync(
			new ImagesCreateParameters
			{
				FromImage = image,
				Tag = tag,
			},
			new(),
			new Progress<JSONMessage>());

	private async Task CreatePostgresContainerAsync()
	{
		var config = new NpgsqlConnectionStringBuilder(ConfigManagerIntegration.TestConnectionString);
		string port = config.Port.ToString();
		string user = config.Username;
		string password = config.Password;
		await _client.Containers.CreateContainerAsync(new()
		{
			Image = FULLIMAGE,
			Name = CONTAINER,
			HostConfig = new()
			{
				PortBindings = new Dictionary<string, IList<PortBinding>>
					{
						{ port, new List<PortBinding> { new() { HostPort = port } } }
					}
			},
			ExposedPorts = new Dictionary<string, EmptyStruct>
				{
					{ port, new() }
				},
			Env = new[]
			{
					$"POSTGRES_USER={user}",
					$"POSTGRES_PASSWORD={password}"
				}
		});
	}

	private async Task WaitForContainerStartsAsync(string container)
	{
		const int maxWait = 10;
		int wait = 0;
		IList<ContainerListResponse> containers;
		do
		{
			if (wait > maxWait)
			{
				throw new TimeoutException("Time for wait container starts exceeded!");
			}

			await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
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

		await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false); // Aguardando inicialização da instância do banco de dados
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing) => _client.Dispose();
}