using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Application.Services;
using Hourly.IntergrationTests.Utilities;
using Hourly.Domain.Entities;


namespace Hourly.IntergrationTests.Domain.Services
{
    public class UserContractServiceTests : IntergrationTestBase
    {
        private IUserContractService _userContractService = null!;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var userContractRepository = new UserContractRepository(_dbContext);
            var userRepository = new UserRepository(_dbContext);
            _userContractService = new UserContractService(userContractRepository, userRepository);
        }
    }
}
