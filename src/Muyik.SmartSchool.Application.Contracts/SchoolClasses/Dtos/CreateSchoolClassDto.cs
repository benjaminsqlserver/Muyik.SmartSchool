using System.ComponentModel.DataAnnotations;

namespace Muyik.SmartSchool.SchoolClasses.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new school class.
    /// </summary>
    public class CreateSchoolClassDto
    {
        /// <summary>
        /// Gets or sets the name of the school class.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the description of the school class.
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }
    }
}
