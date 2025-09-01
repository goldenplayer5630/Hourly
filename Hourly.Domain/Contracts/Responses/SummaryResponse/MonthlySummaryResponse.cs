using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Contracts.Responses.SummaryResponse
{
    public class MonthlySummaryResponse
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TotalRawEffectiveHours { get; set; }
        public double TotalNetEffectiveHours { get; set; }
        public double TotalTVTHoursAccrued { get; set; }
        public double TotalTVTHoursUsed { get; set; }
    }
}
