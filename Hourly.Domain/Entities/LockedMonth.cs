using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Entities
{
    public class LockedMonth
    {
        public Guid Id { get; set; }
        public Guid UserContractId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        [ForeignKey(nameof(UserContractId))]
        public UserContract UserContract { get; set; } = null!;
    }
}
