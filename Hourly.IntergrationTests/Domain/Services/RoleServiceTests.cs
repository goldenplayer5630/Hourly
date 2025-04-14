using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.IntergrationTests.Utilities;
using Hourly.Shared.Entities;
using Hourly.Domain.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
