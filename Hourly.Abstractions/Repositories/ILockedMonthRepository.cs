using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public interface ILockedMonthRepository
    {
        public Task<LockedMonth?> GetById(Guid lockedMonthId);
        public Task<IEnumerable<LockedMonth>> GetAll();
        public Task<IEnumerable<LockedMonth>> Filter(Guid? userContractId, int? year, int? month);
        public Task<LockedMonth> Create(LockedMonth lockedMonth);
        public Task<LockedMonth> Update(LockedMonth lockedMonth);
        public Task Delete(Guid lockedMonthId);
    }
}
