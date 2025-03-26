using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
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

        public async Task<GitRepository> GetById(Guid gitRepositoryId)
        {

        }

        public async Task<IEnumerable<GitRepository>> GetAll()
        {

        }

        public async Task Create(GitRepository gitRepository)
        {

        }

        public async Task Update(GitRepository gitRepository)
        {

        }

        public async Task Delete(GitRepository gitRepositoryId)
        {

        }

    }
}
