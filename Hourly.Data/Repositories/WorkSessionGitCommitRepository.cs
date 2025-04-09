using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class WorkSessionGitCommitRepository : IWorkSessionGitCommitRepository
    {
        private readonly AppDbContext _context;

        public WorkSessionGitCommitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkSessionGitCommit?> GetById(Guid workSessionId, Guid gitCommitId)
        {
            return await _context.WorkSessionGitCommits
                .Include(wsgc => wsgc.WorkSession)
                .Include(wsgc => wsgc.GitCommit)
                .FirstOrDefaultAsync(wsgc => wsgc.WorkSessionId == workSessionId && wsgc.GitCommitId == gitCommitId);
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

        public async Task Delete(Guid workSessionId, Guid gitCommitId)
        {
            var existingWorkSessionGitCommit = await _context.WorkSessionGitCommits.FindAsync(workSessionId, gitCommitId);
            if (existingWorkSessionGitCommit != null)
            {
                _context.WorkSessionGitCommits.Remove(existingWorkSessionGitCommit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
