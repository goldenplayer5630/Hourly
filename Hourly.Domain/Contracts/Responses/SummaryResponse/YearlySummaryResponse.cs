using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Contracts.Responses.SummaryResponse
{
    public class YearlySummaryResponse
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public double TotalRawEffectiveHours { get; set; }
        public double TotalNetEffectiveHours { get; set; }
        public double TotalTVTHoursAccrued { get; set; }
        public double TotalTVTHoursUsed { get; set; }

        // Navigation properties
        public ICollection<MonthlySummaryResponse> MonthlySummaries { get; set; } = new List<MonthlySummaryResponse>();
    }
}
