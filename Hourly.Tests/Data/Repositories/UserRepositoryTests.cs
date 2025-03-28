using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;
using Hourly.Shared.Models;
using Hourly.Data.Repositories;

namespace Hourly.Tests.Data.Repositories
{
    public class UserRepositoryTests : IAsyncLifetime, IDisposable
    {
        private readonly PostgreSqlContainer _postgresContainer;
        private AppDbContext _dbContext;
        private UserRepository _userRepository;

        public UserRepositoryTests()
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

            _userRepository = new UserRepository(_dbContext);
        }

        public async Task DisposeAsync()
        {
            await _postgresContainer.StopAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        [Fact]
        public async Task GetUser_ShouldReturnUserFromDatabase()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "testuser@example.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Assert
            var retrievedUser = await _userRepository.GetById(user.Id);
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Name, retrievedUser.Name);
        }

        [Fact]
        public async Task AddUser_ShouldAddUserToDatabase()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "testuser@example.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.Create(user);

            // Act
            var retrievedUser = await _dbContext.Users.FindAsync(user.Id);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Name, retrievedUser.Name);
        }
    }
}
