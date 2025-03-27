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
    public class GitCommitRepository : IGitCommitRepository
    {
        private readonly AppDbContext _context;

        public GitCommitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GitCommit?> GetById(Guid gitCommitId)
        {
            return await _context.GitCommits.FindAsync(gitCommitId);
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _context.GitCommits.ToListAsync();
        }

        public async Task Create(GitCommit gitCommit)
        {
            await _context.GitCommits.AddAsync(gitCommit);
        }

        public async Task Update(GitCommit gitCommit)
        {
            var existingGitCommit = await _context.GitCommits.FindAsync(gitCommit);
            if (existingGitCommit != null)
            {
                _context.Entry(existingGitCommit).CurrentValues.SetValues(gitCommit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(GitCommit gitCommitId)
        {
            var existingGitCommit = await _context.GitCommits.FindAsync(gitCommitId);
            if (existingGitCommit != null)
            {
                _context.GitCommits.Remove(existingGitCommit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
