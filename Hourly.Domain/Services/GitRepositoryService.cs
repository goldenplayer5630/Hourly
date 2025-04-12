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
    public class GitRepositoryService : IGitRepositoryService
    {
        private readonly IGitRepositoryRepository _repository;

        public GitRepositoryService(IGitRepositoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GitRepository?> GetById(Guid gitRepositoryId)
        {
            return await _repository.GetById(gitRepositoryId);
        }

        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<GitRepository> Create(GitRepository gitRepository)
        {
            return await _repository.Create(gitRepository);
        }

        public async Task<GitRepository> AddGitCommit(Guid gitRepositoryId, Guid gitCommitId)
        {
            return await _repository.AddGitCommit(gitRepositoryId, gitCommitId);
        }

        public async Task<GitRepository> Update(GitRepository gitRepository)
        {
            return await _repository.Update(gitRepository);
        }

        public async Task Delete(Guid gitRepositoryId)
        {
            await _repository.Delete(gitRepositoryId);
        }
    }
}
