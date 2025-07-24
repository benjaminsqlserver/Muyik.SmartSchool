using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Users.Dtos
{
    public class UserDto : EntityDto<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string UserPhoto { get; set; }
        public bool HasLeftSchool { get; set; }
        public string Address { get; set; }
        public Guid? GenderId { get; set; }
        public string GenderName { get; set; }
        public Guid? SchoolClassId { get; set; }
        public string SchoolClassName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

