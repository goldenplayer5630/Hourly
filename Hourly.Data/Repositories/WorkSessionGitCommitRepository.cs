using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class WorkSessionGitCommitRepository : IWorkSessionGitCommitRepository
    {
        Task<WorkSessionGitCommit> GetById(Guid workSessionGitCommitId);
        Task<IEnumerable<WorkSessionGitCommit>> GetAll();
        Task Create(WorkSessionGitCommit workSessionGitCommit);
        Task Update(WorkSessionGitCommit workSessionGitCommit);
        Task Delete(Guid workSessionGitCommitId);
    }
}
