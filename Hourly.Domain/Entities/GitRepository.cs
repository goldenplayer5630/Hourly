using Hourly.Abstractions.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Entities
{
    public class GitRepository : IGitRepository
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

        public IReadOnlyCollection<IGitCommit> GitCommits => _gitCommits.AsReadOnly();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void Update(GitRepository updatedRepository)
        {
            ExtRepositoryId = updatedRepository.ExtRepositoryId;
            Name = updatedRepository.Name;
            Namespace = updatedRepository.Namespace;
            WebUrl = updatedRepository.WebUrl;
            UpdatedAt = DateTime.UtcNow;
            _gitCommits = updatedRepository._gitCommits.ToList();
        }
    }
}
