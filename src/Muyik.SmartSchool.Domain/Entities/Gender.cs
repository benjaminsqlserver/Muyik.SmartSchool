using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Muyik.SmartSchool.Entities
{
    public class Gender : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(50)]
        public string GenderName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        protected Gender()
        {
        }

        public Gender(Guid id, string genderName, string description = null) : base(id)
        {
            GenderName = genderName;
            Description = description;
        }
    }
}
