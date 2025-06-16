using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Application.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class UserServiceTests : IntergrationTestBase
    {
        private IUserService _userService = null!;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var userRepository = new UserRepository(_dbContext);
            var departmentRepository = new DepartmentRepository(_dbContext);
            _userService = new UserService(userRepository, departmentRepository);
        }
    }
}
