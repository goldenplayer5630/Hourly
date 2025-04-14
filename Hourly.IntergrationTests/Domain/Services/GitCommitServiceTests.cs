using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class GitCommitServiceTests : IntergrationTestBase
    {
        private IGitCommitService _gitCommitService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var gitCommitRepository = new GitCommitRepository(_dbContext);
            var gitRepositoryRepository = new GitRepositoryRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            _gitCommitService = new GitCommitService(gitCommitRepository, gitRepositoryRepository, userRepository);
        }
    }
}
