using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class UserRepositoryTests : IntergrationTestBase, IRepositoryTests
    {
        private IUserRepository _userRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.Users.FirstAsync();

            // Act
            var result = await _userRepository.GetById(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existing.Id, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act
            var result = await _userRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.Users.CountAsync();

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@example.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            await _userRepository.Create(entity);
            var result = await _dbContext.Users.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.Users.FirstAsync();
            existing.Name = "Updated Name";

            // Act
            await _userRepository.Update(existing);
            var result = await _dbContext.Users.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.Users.FirstAsync();

            // Act
            await _userRepository.Delete(existing.Id);
            var result = await _dbContext.Users.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
