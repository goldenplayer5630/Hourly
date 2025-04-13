using Hourly.Abstractions.Contracts.Responses.UserResponses;
using Hourly.Abstractions.Contracts.Responses.WorkSessionResponses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Responses.GitCommitResponses
{
    public class GitCommitSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; internal set; }

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
        public string WebUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
