using Hourly.Domain.Services;
using Hourly.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Hourly.IntergrationTests.Utilities;
using Hourly.Abstractions.Services;
using Hourly.Abstractions.Exceptions;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class DepartmentServiceTests : IntergrationTestBase
    {
        private IDepartmentService _departmentService;
        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var repository = new DepartmentRepository(_dbContext);
            _departmentService = new DepartmentService(repository);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.Departments.FirstAsync();
            // Act
            var result = await _departmentService.GetById(existing.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(existing.Id, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenNotExists()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
                await _departmentService.GetById(fakeId);
            });
        }
    }
}
