// File: Muyik.SmartSchool.Application.Contracts/Users/Queries/GetUsersQuery.cs

using MediatR;
using Muyik.SmartSchool.Users.Dtos;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Users.Queries
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of users based on provided filtering and pagination input.
    /// </summary>
    /// <remarks>
    /// This query is handled via MediatR and returns a <see cref="PagedResultDto{T}"/> containing a list of 
    /// <see cref="UserDto"/> objects. The associated input typically includes filters such as search terms,
    /// page number, and page size.
    /// </remarks>
    public class GetUsersQuery : IRequest<PagedResultDto<UserDto>>
    {
        /// <summary>
        /// Gets or sets the input parameters used for querying users, including pagination and filters.
        /// </summary>
        public GetUsersInput Input { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersQuery"/> class with the specified input.
        /// </summary>
        /// <param name="input">The input parameters for retrieving users.</param>
        public GetUsersQuery(GetUsersInput input)
        {
            Input = input;
        }
    }
}
