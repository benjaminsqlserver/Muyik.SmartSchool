using System;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Dtos
{
    /// <summary>
    /// Data Transfer Object representing a school class.
    /// </summary>
    public class SchoolClassDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the school class.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the description of the school class.
        /// </summary>
        public string Description { get; set; }
    }
}