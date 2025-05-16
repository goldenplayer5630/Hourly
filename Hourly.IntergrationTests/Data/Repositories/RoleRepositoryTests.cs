using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class RoleRepositoryTests : IntergrationTestBase
    {
        private IRoleRepository _roleRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _roleRepository = new RoleRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.Roles.FirstAsync();

            // Act
            var result = await _roleRepository.GetById(existing.Id);

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
            var result = await _roleRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.Roles.CountAsync();

            // Act
            var result = await _roleRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "New Role",
                Permissions = "Read, Write"
            };

            // Act
            await _roleRepository.Create(entity);
            var result = await _dbContext.Roles.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.Roles.FirstAsync();
            existing.Name = "Updated Role Name";

            // Act
            await _roleRepository.Update(existing);
            var result = await _dbContext.Roles.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Role Name", result.Name);
        }
    }
}
