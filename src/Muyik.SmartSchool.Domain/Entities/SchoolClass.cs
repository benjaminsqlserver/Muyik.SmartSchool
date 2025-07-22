using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Muyik.SmartSchool.Entities
{
    public class SchoolClass : FullAuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        protected SchoolClass()
        {
        }

        public SchoolClass(Guid id, string className, string description = null) : base(id)
        {
            ClassName = className;
            Description = description;
        }
    }
}
