using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muyik.SmartSchool.Users.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(500)]
        public string UserPhoto { get; set; }

        public bool HasLeftSchool { get; set; } = false;

        [StringLength(300)]
        public string Address { get; set; }

        public Guid? GenderId { get; set; }

        public Guid? SchoolClassId { get; set; }
    }
}