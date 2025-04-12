using Hourly.Abstractions.Repositories;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Tests.Data.Repositories
{
    public class GitCommitRepositoryTests : IntergrationTestBase
    {
        private IGitCommitRepository _gitCommitRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _gitCommitRepository = new GitCommitRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.GitCommits.FirstAsync();

            // Act
            var result = await _gitCommitRepository.GetById(existing.Id);

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
            var result = await _gitCommitRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.GitCommits.CountAsync();

            // Act
            var result = await _gitCommitRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new GitCommit
            {
                Id = Guid.NewGuid(),
                RepositoryId = Guid.NewGuid(),
                ExtCommitId = "258958b9da4fe91f52c62c32eddade9cb8ee4828",
                ExtCommitShortId = "258958b",
                Title = "Initial commit",
                AuthorId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                WebUrl = "http://example.com"
            };

            // Act
            await _gitCommitRepository.Create(entity);
            var result = await _dbContext.GitCommits.FindAsync(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Title, result.Title);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.GitCommits.FirstAsync();

            // Act
            await _gitCommitRepository.Delete(existing.Id);
            var result = await _dbContext.GitCommits.FindAsync(existing.Id);

            // Assert
            Assert.Null(result);
        }
    }
}
