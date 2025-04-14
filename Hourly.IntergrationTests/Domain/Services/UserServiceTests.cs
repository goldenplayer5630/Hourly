using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class UserServiceTests : IntergrationTestBase
    {
        private IUserService _userService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var userRepository = new UserRepository(_dbContext);
            var departmentRepository = new DepartmentRepository(_dbContext);
            var roleRepository = new RoleRepository(_dbContext);
            _userService = new UserService(userRepository, departmentRepository, roleRepository);
        }
    }
}
