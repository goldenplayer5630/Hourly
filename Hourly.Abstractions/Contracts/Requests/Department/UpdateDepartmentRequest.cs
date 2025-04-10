using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Requests.Department
{
    public class UpdateDepartmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
