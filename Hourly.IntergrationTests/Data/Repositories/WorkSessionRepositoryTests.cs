using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class WorkSessionRepositoryTests : IntergrationTestBase
    {
        private WorkSessionRepository _workSessionRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _workSessionRepository = new WorkSessionRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.WorkSessions.FirstAsync();

            // Act
            var result = await _workSessionRepository.GetById(existing.Id);

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
            var result = await _workSessionRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.WorkSessions.CountAsync();

            // Act
            var result = await _workSessionRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var existingUserContract = await _dbContext.UserContracts.FirstAsync();
            var entity = new WorkSession
            {
                Id = Guid.NewGuid(),
                TaskDescription = "New Task",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                Factor = 1.0f,
                WBSO = false,
                OtherRemarks = "No remarks"
            };

            entity.AssignToUserContract(existingUserContract);

            // Act
            await _workSessionRepository.Create(entity);
            var result = await _dbContext.WorkSessions.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.TaskDescription, result.TaskDescription);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.WorkSessions.FirstAsync();
            existing.TaskDescription = "Updated Task Description";

            // Act
            await _workSessionRepository.Update(existing);
            var result = await _dbContext.WorkSessions.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Task Description", result.TaskDescription);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.WorkSessions.FirstAsync();

            // Act
            await _workSessionRepository.Delete(existing.Id);
            var result = await _dbContext.WorkSessions.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
