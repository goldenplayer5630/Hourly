using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Domain.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommit>> GetAll();
        Task<GitCommit?> GetById(Guid id);
        Task Create(GitCommit gitCommit);
        Task Update(GitCommit gitCommit);
        Task Delete(Guid id);
    }
}
