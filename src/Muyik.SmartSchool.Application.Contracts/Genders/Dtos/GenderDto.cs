using System;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Genders.Dtos
{
    /// <summary>
    /// Data Transfer Object representing a gender.
    /// </summary>
    public class GenderDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Gets or sets the name of the gender.
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// Gets or sets the description of the gender.
        /// </summary>
        public string Description { get; set; }
    }
}