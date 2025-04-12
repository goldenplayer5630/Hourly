using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Requests.GitCommitRequests
{
    public class CreateGitCommitRequest
    {
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
    }
}
