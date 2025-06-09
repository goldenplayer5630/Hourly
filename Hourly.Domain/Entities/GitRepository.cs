using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Entities
{
    public class GitRepository
    {
        protected List<GitCommit> _gitCommits = new();

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public IReadOnlyCollection<GitCommit> GitCommits => _gitCommits.AsReadOnly();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
