using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class GitRepositoryServiceTests : IntergrationTestBase
    {
        private IGitRepositoryService? _gitRepositoryService;

        public GitRepositoryServiceTests(PostgresTestContainerFixture fixture) : base(fixture)
        {
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var gitRepositoryRepository = new GitRepositoryRepository(_dbContext);
            _gitRepositoryService = new GitRepositoryService(gitRepositoryRepository);
        }
    }
}
