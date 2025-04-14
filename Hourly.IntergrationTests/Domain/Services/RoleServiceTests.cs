using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Domain.Services;
using Hourly.IntergrationTests.Utilities;

namespace Hourly.IntergrationTests.Domain.Services
{
    public class RoleServiceTests : IntergrationTestBase
    {
        private IRoleService _roleService;

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            var roleRepository = new RoleRepository(_dbContext);
            _roleService = new RoleService(roleRepository);

        }
    }
}
