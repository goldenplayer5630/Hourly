using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Contracts.Responses.LockedMonthResponses
{
    public class LockedMonthResponse : LockedMonthSummaryResponse
    {
        public UserContract UserContract { get; set; } = null!;
    }
}
