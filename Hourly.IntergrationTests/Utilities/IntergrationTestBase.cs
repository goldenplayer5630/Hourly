using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Hourly.IntergrationTests.Utilities
{
    public abstract class IntergrationTestBase : IAsyncLifetime, IClassFixture<PostgresTestContainerFixture>
    {
        protected AppDbContext _dbContext = null!;
        protected readonly PostgresTestContainerFixture _fixture;

        public IntergrationTestBase(PostgresTestContainerFixture fixture)
        {
            _fixture = fixture;
        }

        public virtual async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_fixture.ConnectionString)
                .Options;

            _dbContext = new AppDbContext(options);

            // Delete existing data if necessary
            await _dbContext.Database.EnsureDeletedAsync();

            // Create the database
            await _dbContext.Database.MigrateAsync();

            await TestDatabaseSeeder.SeedAsync(_dbContext);
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
