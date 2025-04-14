using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class GitRepositoryServiceTests : IntergrationTestBase
    {
        private IGitRepositoryService _gitRepositoryService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var gitRepositoryRepository = new GitRepositoryRepository(_dbContext);
            _gitRepositoryService = new GitRepositoryService(gitRepositoryRepository);
        }
    }
}
