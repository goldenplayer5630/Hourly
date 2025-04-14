using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
