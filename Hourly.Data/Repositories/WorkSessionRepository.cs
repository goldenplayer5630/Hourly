using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        private readonly AppDbContext _context;

        public WorkSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkSession?> GetById(Guid workSessionId)
        {
            return await _context.WorkSessions
                .Include(ws => ws.User)
                .Include(ws => ws.WorkSessionGitCommits)
                    .ThenInclude(wsgc => wsgc.GitCommit)
                .FirstOrDefaultAsync(ws => ws.Id == workSessionId);
        }

        public async Task<IEnumerable<WorkSession>> GetAll()
        {
            return await _context.WorkSessions
                .Include(ws => ws.User)
                .Include(ws => ws.WorkSessionGitCommits)
                    .ThenInclude(wsgc => wsgc.GitCommit)
                .ToListAsync();
        }

        public async Task Create(WorkSession workSession)
        {
            await _context.WorkSessions.AddAsync(workSession);
            await _context.SaveChangesAsync();
        }

        public async Task Update(WorkSession workSession)
        {
            var existingWorkSession = await _context.WorkSessions.FindAsync(workSession.Id);
            if (existingWorkSession != null)
            {
                _context.Entry(existingWorkSession).CurrentValues.SetValues(workSession);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid workSessionId)
        {
            var workSession = await _context.WorkSessions.FindAsync(workSessionId);
            if (workSession != null)
            {
                _context.WorkSessions.Remove(workSession);
                await _context.SaveChangesAsync();
            }
        }
    }
}
