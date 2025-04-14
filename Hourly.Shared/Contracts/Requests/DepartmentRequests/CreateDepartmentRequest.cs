using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.DepartmentRequests
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
