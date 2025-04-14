using Hourly.Abstractions.Exceptions;
using Hourly.Abstractions.Repositories;
using Hourly.Shared.Entities;
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
            return await _context.WorkSessions.FindAsync(workSessionId);
        }

        public async Task<IEnumerable<WorkSession>> GetAll()
        {
            return await _context.WorkSessions.ToListAsync();
        }

        public async Task<WorkSession> Create(WorkSession workSession)
        {
            await _context.WorkSessions.AddAsync(workSession);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? workSession : null) ?? throw new InvalidOperationException();
        }

        public async Task<WorkSession> Update(WorkSession workSession)
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
