using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Domain.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSession>> GetAll();
        Task<WorkSession?> GetById(Guid id);
        Task Create(WorkSession workSession);
        Task Update(WorkSession workSession);
        Task Delete(Guid id);
    }
}
