using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Hourly.Data;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Hourly.Tests.Data.Repositories
{
    public class WorkSessionRepositoryTests
    {
        private readonly PostgreSqlContainer _postgresContainer;
        private AppDbContext _dbContext = null!;

        public WorkSessionRepositoryTests()
        {
            _postgresContainer = new PostgreSqlBuilder().Build();
        }

        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_postgresContainer.GetConnectionString())
                .Options;

            _dbContext = new AppDbContext(options);
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await _postgresContainer.StopAsync();
        }
    }
}
