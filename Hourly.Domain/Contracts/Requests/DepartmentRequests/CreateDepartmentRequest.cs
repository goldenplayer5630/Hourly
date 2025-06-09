using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Contracts.Requests.DepartmentRequests
{
    public class CreateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
