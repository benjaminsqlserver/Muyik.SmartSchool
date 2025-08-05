using MediatR;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Queries
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of school classes based on provided filtering and pagination input.
    /// </summary>
    /// <remarks>
    /// This query is handled via MediatR and returns a PagedResultDto containing a list of 
    /// SchoolClassDto objects.
    /// </remarks>
    public class GetSchoolClassesQuery : IRequest<PagedResultDto<SchoolClassDto>>
    {
        /// <summary>
        /// Gets or sets the input parameters used for querying school classes, including pagination and filters.
        /// </summary>
        public GetSchoolClassesInput Input { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSchoolClassesQuery"/> class.
        /// </summary>
        /// <param name="input">The input parameters for retrieving school classes.</param>
        public GetSchoolClassesQuery(GetSchoolClassesInput input)
        {
            Input = input;
        }
    }
}