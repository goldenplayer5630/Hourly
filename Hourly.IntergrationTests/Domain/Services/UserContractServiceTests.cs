using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;


namespace Hourly.IntergrationTests.Domain.Services
{
    public class UserContractServiceTests : IntergrationTestBase
    {
        private IUserContractService _userContractService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var userContractRepository = new UserContractRepository(_dbContext);
            _userContractService = new UserContractService(userContractRepository);
        }
    }
}
