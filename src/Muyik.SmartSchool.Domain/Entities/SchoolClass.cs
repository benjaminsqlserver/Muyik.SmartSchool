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
            // Only for EF Core
        }

        public SchoolClass(Guid id, string className, string description = null) : base(id)
        {
            SetClassName(className);
            SetDescription(description);
        }

        /// <summary>
        /// Sets the class name with domain validation
        /// </summary>
        public void SetClassName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("Class name cannot be null, empty, or whitespace.", nameof(className));
            }

            if (className.Length > 100)
            {
                throw new ArgumentException("Class name cannot exceed 100 characters.", nameof(className));
            }

            ClassName = className.Trim();
        }

        /// <summary>
        /// Sets the description with domain validation
        /// </summary>
        public void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 200)
            {
                throw new ArgumentException("Description cannot exceed 200 characters.", nameof(description));
            }

            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        }
    }
}