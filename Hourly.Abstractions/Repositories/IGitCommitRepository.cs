using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public interface IGitCommitRepository
    {
        Task<GitCommit> GetById(Guid gitCommitId);
        Task<IEnumerable<GitCommit>> GetAll();
        Task Create(GitCommit gitCommit);
        Task Update(GitCommit gitCommit);
        Task Delete(GitCommit gitCommitId);
    }
}
