using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<WorkSessionGitCommit?> GetById(Guid workSessionGitCommitId)
        {
            return await _context.WorkSessionGitCommits
                .Include(wsgc => wsgc.WorkSession)
                .Include(wsgc => wsgc.GitCommit)
                .FirstOrDefaultAsync(wsgc => wsgc.WorkSessionId == workSessionGitCommitId);
        }

        public async Task<IEnumerable<WorkSessionGitCommit>> GetAll()
        {
            return await _context.WorkSessionGitCommits
                .Include(wsgc => wsgc.WorkSession)
                .Include(wsgc => wsgc.GitCommit)
                .ToListAsync();
        }

        public async Task Create(WorkSessionGitCommit workSessionGitCommit)
        {
            await _context.WorkSessionGitCommits.AddAsync(workSessionGitCommit);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WorkSessionGitCommit workSessionGitCommit)
        {
            var existingWorkSessionGitCommit = await _context.Users.FindAsync(workSessionGitCommit);
            if (existingWorkSessionGitCommit != null)
            {
                _context.Entry(existingWorkSessionGitCommit).CurrentValues.SetValues(workSessionGitCommit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid workSessionGitCommitId)
        {
            var existingWorkSessionGitCommit = await _context.WorkSessionGitCommits.FindAsync(workSessionGitCommitId);
            if (existingWorkSessionGitCommit != null)
            {
                _context.WorkSessionGitCommits.Remove(existingWorkSessionGitCommit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
