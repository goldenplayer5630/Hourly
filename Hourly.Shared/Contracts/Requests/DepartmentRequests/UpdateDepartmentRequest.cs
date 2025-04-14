using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.DepartmentRequests
{
    public class UpdateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
