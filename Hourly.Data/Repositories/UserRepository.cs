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
        Task<User> GetById(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task Create(User user);
        Task Update(User user);
        Task Delete(Guid userId);
    }
}
