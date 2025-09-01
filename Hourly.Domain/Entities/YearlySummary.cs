using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Entities
{
    public class YearlySummary
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public double TotalRawEffectiveHours { get; set; }
        public double TotalNetEffectiveHours { get; set; }
        public double TotalTVTHoursAccrued { get; set; }
        public double TotalTVTHoursUsed { get; set; }

        // Navigation properties
        public ICollection<MonthlySummary> MonthlySummaries { get; set; } = new List<MonthlySummary>();
    }
}
