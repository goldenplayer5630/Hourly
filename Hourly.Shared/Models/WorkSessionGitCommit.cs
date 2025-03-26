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

        [ForeignKey("WorkSessionId")]
        public WorkSession WorkSession { get; set; }

        [ForeignKey("GitCommitId")]
        public GitCommit GitCommit { get; set; }
    }
}
