using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface IGitRepositoryService
    {
        Task<IEnumerable<GitRepository>> GetAll();
        Task<GitRepository?> GetById(Guid id);
        Task Create(GitRepository gitRepository);
        Task Update(GitRepository gitRepository);
        Task Delete(Guid id);
    }
}
