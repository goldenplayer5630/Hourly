using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid ManagerId { get; set; }

        public User Manager { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
