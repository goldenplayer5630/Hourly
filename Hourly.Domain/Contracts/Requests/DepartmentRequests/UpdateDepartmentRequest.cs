using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Contracts.Requests.DepartmentRequests
{
    public class UpdateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
