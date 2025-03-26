using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class GitRepositoryRepository : IGitRepositoryRepository
    {
        Task<GitRepository> GetById(Guid gitRepositoryId);
        Task<IEnumerable<GitRepository>> GetAll();
        Task Create(GitRepository gitRepository);
        Task Update(GitRepository gitRepository);
        Task Delete(GitRepository gitRepositoryId);
    }
}
