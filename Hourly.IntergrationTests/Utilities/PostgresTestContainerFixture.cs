using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace Hourly.IntergrationTests.Utilities
{
    public class PostgresTestContainerFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _container;
        public string ConnectionString => _container.GetConnectionString();

        public PostgresTestContainerFixture()
        {
            _container = new PostgreSqlBuilder().Build();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }
    }
}
