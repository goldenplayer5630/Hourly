using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Application.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class WorkSessionServiceTests : IntergrationTestBase
    {
        private IWorkSessionService _workSessionService = null!;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();

            var workSessionRepository = new WorkSessionRepository(_dbContext);
            var gitCommitRepository = new GitCommitRepository(_dbContext);
            var userContractRepository = new UserContractRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            var lockedMonthRepository = new LockedMonthRepository(_dbContext);
            var gitRepositoryRepository = new GitRepositoryRepository(_dbContext);

            var gitCommitService = new GitCommitService(gitCommitRepository, gitRepositoryRepository, userRepository);
            var userContractService = new UserContractService(userContractRepository, userRepository, lockedMonthRepository);
            var workSessionService = new WorkSessionService(workSessionRepository, gitCommitService, userContractService);
            _workSessionService = workSessionService;
        }
    }
}
