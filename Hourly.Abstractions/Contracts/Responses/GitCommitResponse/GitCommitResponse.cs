using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Contracts.Responses.GitCommitResponse
{
    public class GitCommitResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid RepositoryId { get; init; }

        [Required]
        public string ExtCommitId { get; init; }

        [Required]
        public string ExtCommitShortId { get; init; }

        [Required]
        public string Title { get; init; }

        public string? Comment { get; init; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public string WebUrl { get; init; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
