using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Services
{
    public class GitCommitService : IGitCommitService
    {
        private readonly IGitCommitRepository _repository;

        public GitCommitService(IGitCommitRepository repository)
        {
            _repository = repository;
        }

        public async Task<GitCommit?> GetById(Guid gitCommitId)
        {
            return await _repository.GetById(gitCommitId);
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<GitCommit> Create(GitCommit gitCommit)
        {
            gitCommit.Id = Guid.NewGuid();
            gitCommit.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(gitCommit);
        }

        public async Task Delete(Guid gitCommitId)
        {
            await _repository.Delete(gitCommitId);
        }
    }
}
