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
        private readonly AppDbContext _context;

        public WorkSessionGitCommitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkSessionGitCommit> GetById(Guid workSessionGitCommitId)
        {

        }

        public async Task<IEnumerable<WorkSessionGitCommit>> GetAll()
        {

        }

        public async Task Create(WorkSessionGitCommit workSessionGitCommit)
        {

        }

        public async Task Update(WorkSessionGitCommit workSessionGitCommit)
        {

        }

        public async Task Delete(Guid workSessionGitCommitId)
        {

        }
    }
}
