using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class DepartmentRepositoryTests : IntergrationTestBase
    {
        private IDepartmentRepository _departmentRepository = null!;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _departmentRepository = new DepartmentRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.Departments.FirstAsync();

            // Act
            var result = await _departmentRepository.GetById(existing.Id);

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
            var result = await _departmentRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.Departments.CountAsync();

            // Act
            var result = await _departmentRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new Department
            {
                Id = Guid.NewGuid(),
                Name = "Finance",
            };

            // Act
            await _departmentRepository.Create(entity);
            var result = await _dbContext.Departments.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.Departments.FirstAsync();
            existing.Name = "Sales 2025";

            // Act

            await _departmentRepository.Update(existing);
            var result = await _dbContext.Departments.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sales 2025", result.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.Departments.FirstAsync();

            // Act
            await _departmentRepository.Delete(existing.Id);
            var result = await _dbContext.Departments.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
