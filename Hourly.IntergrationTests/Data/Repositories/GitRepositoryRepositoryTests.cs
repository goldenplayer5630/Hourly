using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class GitRepositoryRepositoryTests : IntergrationTestBase
    {
        private IGitRepositoryRepository _gitRepositoryRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _gitRepositoryRepository = new GitRepositoryRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.GitRepositories.FirstAsync();

            // Act
            var result = await _gitRepositoryRepository.GetById(existing.Id);

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
            var result = await _gitRepositoryRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.GitRepositories.CountAsync();

            // Act
            var result = await _gitRepositoryRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new GitRepository
            {
                Id = Guid.NewGuid(),
                ExtRepositoryId = "8a7a9345eea565973918b5503d81dcd52637f3a4",
                Name = "Hourly",
                Namespace = "goldenplayer5630/hourly",
                WebUrl = "https://github.com/goldenplayer5630/Hourly"
            };

            // Act
            await _gitRepositoryRepository.Create(entity);
            var result = await _dbContext.GitRepositories.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.GitRepositories.FirstAsync();
            existing.Name = "Updated Repository Name";

            // Act
            await _gitRepositoryRepository.Update(existing);
            var result = await _dbContext.GitRepositories.FindAsync(existing.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Repository Name", result.Name);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.GitRepositories.FirstAsync();

            // Act
            await _gitRepositoryRepository.Delete(existing.Id);
            var result = await _dbContext.GitRepositories.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
