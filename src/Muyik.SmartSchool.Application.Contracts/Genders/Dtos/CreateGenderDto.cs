using System.ComponentModel.DataAnnotations;

namespace Muyik.SmartSchool.Genders.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new gender.
    /// </summary>
    public class CreateGenderDto
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