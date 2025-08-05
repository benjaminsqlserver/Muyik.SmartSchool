using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Dtos
{
    /// <summary>
    /// Input DTO for retrieving a paginated and filtered list of school classes.
    /// </summary>
    public class GetSchoolClassesInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Gets or sets the filter string for searching school classes by name or description.
        /// </summary>
        public string Filter { get; set; }
    }
}