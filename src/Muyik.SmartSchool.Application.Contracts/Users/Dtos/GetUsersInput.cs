using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Users.Dtos
{
    public class GetUsersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? SchoolClassId { get; set; }
        public bool? HasLeftSchool { get; set; }
    }
}
