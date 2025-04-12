using Hourly.Abstractions.Repositories;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Hourly.Abstractions.Exceptions;

namespace Hourly.Data.Repositories
{
    public class GitCommitRepository : IGitCommitRepository
    {
        private readonly AppDbContext _context;

        public GitCommitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GitCommit?> GetById(Guid gitCommitId)
        {
            var result = await _context.GitCommits.FindAsync(gitCommitId);

            if (result == null)
            {
                throw new EntityNotFoundException("Git commit not found!");
            }

            return result;
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _context.GitCommits.ToListAsync();
        }

        public async Task<GitCommit> Create(GitCommit gitCommit)
        {
            await _context.GitCommits.AddAsync(gitCommit);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? gitCommit : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid gitCommitId)
        {
            var existingGitCommit = await _context.GitCommits.FindAsync(gitCommitId);

            if (existingGitCommit == null)
            {
                throw new EntityNotFoundException("Git commit not found!");
            }

            _context.GitCommits.Remove(existingGitCommit);
            await _context.SaveChangesAsync();
        }
    }
}
