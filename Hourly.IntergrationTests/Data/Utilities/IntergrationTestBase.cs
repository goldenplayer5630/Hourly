using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Hourly.Tests.Data.Utilities
{
    public abstract class IntergrationTestBase : IAsyncLifetime
    {
        protected AppDbContext _dbContext = null!;
        private readonly PostgreSqlContainer _postgresContainer;

        public IntergrationTestBase()
        {
            _postgresContainer = new PostgreSqlBuilder().Build();
        }

        public virtual async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_postgresContainer.GetConnectionString())
                .Options;

            _dbContext = new AppDbContext(options);

            await _dbContext.Database.MigrateAsync();

            await TestDatabaseSeeder.SeedAsync(_dbContext);
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
            await _postgresContainer.StopAsync();
        }
    }
}
