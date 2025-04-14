using Hourly.Abstractions.Services;
using Hourly.IntergrationTests.Utilities;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class UserServiceTests : IntergrationTestBase
    {
        private IUserService? _userService;

        public UserServiceTests(PostgresTestContainerFixture fixture) : base(fixture)
        {
        }

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
