using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionRepository
    {
        Task<WorkSession?> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> GetAll();
        Task Create(WorkSession workSession);
        Task Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
    }
}
