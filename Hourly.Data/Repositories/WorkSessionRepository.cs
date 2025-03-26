using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        public async Task<WorkSession> GetById(Guid workSessionId)
        {

        }

        public async Task<IEnumerable<WorkSession>> GetAll();
        public async Task Create(WorkSession workSession);
        public async Task Update(WorkSession workSession);
        public async Task Delete(Guid workSessionId);
    }
}
