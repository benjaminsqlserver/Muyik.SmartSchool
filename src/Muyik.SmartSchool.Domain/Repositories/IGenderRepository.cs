using Muyik.SmartSchool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Muyik.SmartSchool.Repositories
{
    public interface IGenderRepository : IRepository<Gender, Guid>
    {
    }
}
