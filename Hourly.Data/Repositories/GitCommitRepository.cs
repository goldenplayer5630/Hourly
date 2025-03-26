using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class GitCommitRepository : IGitCommitRepository
    {
        public async Task<GitCommit> GetById(Guid gitCommitId);
        public async Task<IEnumerable<GitCommit>> GetAll();
        public async Task Create(GitCommit gitCommit);
        public async Task Update(GitCommit gitCommit);
        public async Task Delete(GitCommit gitCommitId);
    }
}
