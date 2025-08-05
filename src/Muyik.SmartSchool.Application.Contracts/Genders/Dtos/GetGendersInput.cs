using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Genders.Dtos
{
    /// <summary>
    /// Input DTO for retrieving a paginated and filtered list of genders.
    /// </summary>
    public class GetGendersInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter string for searching genders by name or description.
        /// </summary>
        public string Filter { get; set; }
    }
}