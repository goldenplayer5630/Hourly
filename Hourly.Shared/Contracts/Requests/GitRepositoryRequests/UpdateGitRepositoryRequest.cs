using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.GitRepositoryRequests
{
    public class UpdateGitRepositoryRequest
    {
        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string WebUrl { get; set; }
    }
}
