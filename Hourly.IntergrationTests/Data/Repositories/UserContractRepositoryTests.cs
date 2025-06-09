using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hourly.IntergrationTests.Data.Repositories
{
    public class UserContractRepositoryTests : IntergrationTestBase
    {
        private IUserContractRepository _userContractRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _userContractRepository = new UserContractRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.UserContracts.FirstAsync();

            // Act
            var result = await _userContractRepository.GetById(existing.Id);

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
            var result = await _userContractRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.UserContracts.CountAsync();

            // Act
            var result = await _userContractRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddNewUserContract_WithRequiredFields()
        {
            // Arrange
            var user = await _dbContext.Users.FirstAsync();
            var entity = new UserContract
            {
                UserId = user.Id,
                Name = "Integration Test Contract",
                ContractType = ContractType.FullTime,
                IsActive = true,
                MinWeeklyHours = 160,
                MaxWeeklyHours = 200,
                GrossHourlyRate = 50.0f,
                HolidayHoursPercentage = 10,
                MonthlyPaidHolidayHours = true,
                StartDate = DateTime.UtcNow.Date,
                Description = "Integration test contract creation",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            await _userContractRepository.Create(entity);
            var result = await _dbContext.UserContracts.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.UserId, result.UserId);
            Assert.Equal(entity.ContractType, result.ContractType);
            Assert.True(result.IsActive);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.UserContracts.FirstAsync();
            existing.Name = "Updated UserContract Name";

            // Act
            await _userContractRepository.Update(existing);
            var result = await _dbContext.UserContracts.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated UserContract Name", result.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.UserContracts.FirstAsync();

            // Act
            await _userContractRepository.Delete(existing.Id);
            var result = await _dbContext.UserContracts.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
