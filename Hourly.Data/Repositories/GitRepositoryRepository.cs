using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class GitRepositoryRepository : IGitRepositoryRepository
    {
        private readonly AppDbContext _context;

        public GitRepositoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GitRepository?> GetById(Guid gitRepositoryId)
        {
            return await _context.GitRepositories.FindAsync(gitRepositoryId);
        }

        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return await _context.GitRepositories.ToListAsync();
        }

        public async Task Create(GitRepository gitRepository)
        {
            await _context.GitRepositories.AddAsync(gitRepository);
            await _context.SaveChangesAsync();
        }

        public async Task Update(GitRepository gitRepository)
        {
            var existingGitRepository = await _context.Roles.FindAsync(gitRepository);
            if (existingGitRepository != null)
            {
                _context.Entry(existingGitRepository).CurrentValues.SetValues(gitRepository);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(GitRepository gitRepositoryId)
        {
            var existingGitRepository = await _context.GitRepositories.FindAsync(gitRepositoryId);
            if (existingGitRepository != null)
            {
                _context.GitRepositories.Remove(existingGitRepository);
                await _context.SaveChangesAsync();
            }
        }
    }
}
