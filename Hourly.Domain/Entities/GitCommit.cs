using Hourly.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Entities
{
    public class GitCommit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; private set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepository Repository { get; private set; }

        [Required]
        public string ExtCommitId { get; set; }

        [Required]
        public string ExtCommitShortId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Comment { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AssignToRepository(GitRepository repository)
        {
            if (RepositoryId == repository.Id)
            {
                throw new DomainValidationException("Commit is already part of this repository.");
            }

            RepositoryId = repository.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignToAuthor(User author)
        {
            if (AuthorId == author.Id)
            {
                throw new DomainValidationException("Commit is already assigned to this author.");
            }
            AuthorId = author.Id;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
