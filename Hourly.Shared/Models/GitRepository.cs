using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class GitRepository
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string WebUrl { get; set; }

        public ICollection<GitCommit> GitCommits { get; set; }
    }
}
