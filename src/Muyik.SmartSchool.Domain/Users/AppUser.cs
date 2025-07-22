using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Users
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(500)]
        public string? UserPhoto { get; set; } // Store file path or URL

        public bool? HasLeftSchool { get; set; } = false;

        [StringLength(300)]
        public string? Address { get; set; }

        // Foreign Keys
        public Guid? GenderId { get; set; }
        public Guid? SchoolClassId { get; set; }

        // Navigation Properties
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [ForeignKey("SchoolClassId")]
        public virtual SchoolClass SchoolClass { get; set; }

        public AppUser()
        {
        }

        public AppUser(Guid id, string userName, string email, Guid? tenantId = null)
            : base(id, userName, email, tenantId)
        {
        }

        // Constructor with extended properties
        public AppUser(
            Guid id,
            string userName,
            string email,
            string firstName = null,
            string middleName = null,
            DateTime? dateOfBirth = null,
            string userPhoto = null,
            bool hasLeftSchool = false,
            string address = null,
            Guid? genderId = null,
            Guid? schoolClassId = null,
            Guid? tenantId = null)
            : base(id, userName, email, tenantId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
            UserPhoto = userPhoto;
            HasLeftSchool = hasLeftSchool;
            Address = address;
            GenderId = genderId;
            SchoolClassId = schoolClassId;
        }
    }
}