using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommit>> GetAll();
        Task<GitCommit?> GetById(Guid id);
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid id);
    }
}
