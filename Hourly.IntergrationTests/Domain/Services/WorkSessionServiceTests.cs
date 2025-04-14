using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class WorkSessionServiceTests : IntergrationTestBase
    {
        private IWorkSessionService _workSessionService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var workSessionRepository = new WorkSessionRepository(_dbContext);
            var gitCommitRepository = new GitCommitRepository(_dbContext);
            _workSessionService = new WorkSessionService(workSessionRepository, gitCommitRepository);
        }
    }
}
