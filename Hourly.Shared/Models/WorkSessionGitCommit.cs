using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class WorkSessionGitCommit
    {
        [Key]
        public Guid WorkSessionId { get; set; }

        [Key]
        public Guid GitCommitId { get; set; }

        [Required]
        [ForeignKey("WorkSessionId")]
        public WorkSession WorkSession { get; set; }

        [Required]
        [ForeignKey("GitCommitId")]
        public GitCommit GitCommit { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
