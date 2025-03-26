using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Shared.Models;
using Hourly.Abstractions.Repositories;

namespace Hourly.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetById(Guid userId);
        public async Task<IEnumerable<User>> GetAll();
        public async Task Create(User user);
        public async Task Update(User user);
        public async Task Delete(Guid userId);
    }
}
