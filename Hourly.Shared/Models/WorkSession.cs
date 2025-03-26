using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class WorkSession
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string TaskDescription { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public float Factor { get; set; }

        public bool WBSO { get; set; }

        public string OtherRemarks { get; set; }

        public ICollection<WorkSessionGitCommit> WorkSessionGitCommits { get; set; }
    }
}
