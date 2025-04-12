using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSession>> GetAll();
        Task<WorkSession?> GetById(Guid workSessionId);
        Task Create(WorkSession workSession);
        Task Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
    }
}
