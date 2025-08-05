using System.ComponentModel.DataAnnotations;

namespace Muyik.SmartSchool.Genders.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating an existing gender.
    /// </summary>
    public class UpdateGenderDto
    {
        /// <summary>
        /// Gets or sets the name of the gender.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string GenderName { get; set; }

        /// <summary>
        /// Gets or sets the description of the gender.
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }
    }
}