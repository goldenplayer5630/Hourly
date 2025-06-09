using Hourly.Abstractions.Entities;
using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Entities
{
    public class GitCommit : IGitCommit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; private set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public IGitRepository Repository { get; private set; }

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
        public IUser Author { get; set; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public IReadOnlyCollection<IWorkSession> WorkSessions { get; set; } = new List<IWorkSession>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AssignToRepository(IGitRepository repository)
        {
            if (RepositoryId == repository.Id)
            {
                throw new DomainValidationException("Commit is already part of this repository.");
            }

            RepositoryId = repository.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignToAuthor(IUser author)
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
