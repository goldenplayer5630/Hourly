using Hourly.Tests.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Data.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Hourly.Abstractions.Repositories;

namespace Hourly.Tests.Data.Repositories
{
    public class WorkSessionGitCommitRepositoryTests : IntergrationTestBase, IRepositoryTests
    {
        private IWorkSessionGitCommitRepository _workSessionGitCommitRepository;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            _workSessionGitCommitRepository = new WorkSessionGitCommitRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var existing = await _dbContext.WorkSessionGitCommits.FirstAsync();

            // Act
            var result = await _workSessionGitCommitRepository.GetById(existing.WorkSessionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existing.WorkSessionId, result.WorkSessionId);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act
            var result = await _workSessionGitCommitRepository.GetById(fakeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var count = await _dbContext.WorkSessionGitCommits.CountAsync();

            // Act
            var result = await _workSessionGitCommitRepository.GetAll();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            // Arrange
            var entity = new WorkSessionGitCommit
            {
                WorkSessionId = Guid.NewGuid(),
                GitCommitId = Guid.NewGuid()
            };

            // Act
            await _workSessionGitCommitRepository.Create(entity);
            var result = await _dbContext.WorkSessionGitCommits.FindAsync(entity.WorkSessionId, entity.GitCommitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.WorkSessionId, result.WorkSessionId);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var existing = await _dbContext.WorkSessionGitCommits.FirstAsync();
            existing.GitCommitId = Guid.NewGuid();

            // Act
            await _workSessionGitCommitRepository.Update(existing);
            var result = await _dbContext.WorkSessionGitCommits.FindAsync(existing.WorkSessionId, existing.GitCommitId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existing.GitCommitId, result.GitCommitId);
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            // Arrange
            var existing = await _dbContext.WorkSessionGitCommits.FirstAsync();

            // Act
            await _workSessionGitCommitRepository.Delete(existing.WorkSessionId);
            var result = await _dbContext.WorkSessionGitCommits.FindAsync(existing.WorkSessionId, existing.GitCommitId);

            // Assert
            Assert.Null(result);
        }
    }
}
