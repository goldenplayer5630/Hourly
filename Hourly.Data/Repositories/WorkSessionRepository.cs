using Hourly.Abstractions.Entities;
using Hourly.Abstractions.Repositories;
using Hourly.Shared.Exceptions;
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

        public async Task<IWorkSession?> GetById(Guid workSessionId)
        {
            return await _context.WorkSessions
                .Include(ws => ws.GitCommits)
                    .ThenInclude(gc => gc.Repository)
                .Include(ws => ws.GitCommits)
                    .ThenInclude(gc => gc.Author)
                    .ThenInclude(u => u.Role)
                .Include(ws => ws.UserContract)
                .FirstOrDefaultAsync(ws => ws.Id == workSessionId);
        }

        public async Task<IEnumerable<IWorkSession>> GetAll()
        {
            return await _context.WorkSessions.ToListAsync();
        }

        public async Task<IEnumerable<IWorkSession>> Filter(Guid? userContractId, int? year, int? month, bool? wbso)
        {
            var query = _context.WorkSessions
                .Include(ws => ws.UserContract)
                .AsQueryable();

            if (userContractId.HasValue)
                query = query.Where(ws => ws.UserContractId == userContractId.Value);

            if (year.HasValue)
                query = query.Where(ws => ws.StartTime.Year == year.Value);

            if (month.HasValue)
                query = query.Where(ws => ws.StartTime.Month == month.Value);

            if (wbso.HasValue)
                query = query.Where(ws => ws.WBSO == wbso.Value);

            return await query.ToListAsync();
        }

        public async Task<IWorkSession> Create(IWorkSession workSession)
        {
            await _context.WorkSessions.AddAsync(workSession);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? workSession : null) ?? throw new InvalidOperationException();
        }

        public async Task<IWorkSession> Update(IWorkSession workSession)
        {
            var existingWorkSession = await _context.WorkSessions.FindAsync(workSession.Id);
            if (existingWorkSession == null)
            {
                throw new EntityNotFoundException("Work session not found!");
            }

            _context.Entry(existingWorkSession).CurrentValues.SetValues(workSession);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? workSession : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid workSessionId)
        {
            var workSession = await _context.WorkSessions.FindAsync(workSessionId);
            if (workSession == null)
            {
                throw new EntityNotFoundException("Work session not found!");
            }

            _context.WorkSessions.Remove(workSession);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
