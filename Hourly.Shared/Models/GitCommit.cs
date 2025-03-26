using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class GitCommit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; set; }

        [ForeignKey("RepositoryId")]
        public GitRepository Repository { get; set; }

        public Guid ExtCommitId { get; set; }

        public string ExtCommitShortId { get; set; }

        public string Title { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public string WebUrl { get; set; }

        public ICollection<WorkSessionGitCommit> WorkSessionGitCommits { get; set; }
    }
}
