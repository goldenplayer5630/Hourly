using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionGitCommitRepository
    {
        Task<WorkSessionGitCommit> GetById(Guid workSessionGitCommitId);
        Task<IEnumerable<WorkSessionGitCommit>> GetAll();
        Task Create(WorkSessionGitCommit workSessionGitCommit);
        Task Update(WorkSessionGitCommit workSessionGitCommit);
        Task Delete(Guid workSessionGitCommitId);
    }
}
