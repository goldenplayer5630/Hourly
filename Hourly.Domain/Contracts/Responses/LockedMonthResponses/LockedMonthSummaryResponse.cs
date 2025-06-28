using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Contracts.Responses.LockedMonthResponses
{
    public class LockedMonthSummaryResponse
    {
        public Guid Id { get; set; }
        public Guid UserContractId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
