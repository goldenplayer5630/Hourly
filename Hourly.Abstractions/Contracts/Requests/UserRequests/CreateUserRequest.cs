using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Requests.UserRequests
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

    }
}
