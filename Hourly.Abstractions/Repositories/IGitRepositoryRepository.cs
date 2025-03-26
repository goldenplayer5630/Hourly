using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitRepositoryRepository
    {
        Task<GitRepository> GetById(Guid gitRepositoryId);
        Task<IEnumerable<GitRepository>> GetAll();
        Task Create(GitRepository gitRepository);
        Task Update(GitRepository gitRepository);
        Task Delete(GitRepository gitRepositoryId);
    }
}
