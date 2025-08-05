using MediatR;
using Muyik.SmartSchool.Genders.Dtos;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Genders.Queries
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of genders based on provided filtering and pagination input.
    /// </summary>
    /// <remarks>
    /// This query is handled via MediatR and returns a PagedResultDto containing a list of 
    /// GenderDto objects.
    /// </remarks>
    public class GetGendersQuery : IRequest<PagedResultDto<GenderDto>>
    {
        /// <summary>
        /// Gets or sets the input parameters used for querying genders, including pagination and filters.
        /// </summary>
        public GetGendersInput Input { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGendersQuery"/> class.
        /// </summary>
        /// <param name="input">The input parameters for retrieving genders.</param>
        public GetGendersQuery(GetGendersInput input)
        {
            Input = input;
        }
    }
}
