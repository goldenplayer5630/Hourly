using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Permissions { get; set; } // Store as JSON string

        public ICollection<User> Users { get; set; }
    }
}
