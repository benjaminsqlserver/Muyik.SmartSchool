using AutoMapper.Internal.Mappers;
using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Genders.Dtos;
using Muyik.SmartSchool.Genders.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace Muyik.SmartSchool.Genders.QueryHandlers
{
    /*
     GetGendersQueryHandler
     ----------------------
     This class handles queries for retrieving a paginated list of "Gender" entities 
     from the data repository, applying optional filtering and sorting. 
     It is an application service implementing the MediatR request handler interface 
     for `GetGendersQuery` requests, returning a `PagedResultDto<GenderDto>`.

     Key Responsibilities:
     - Receives a `GetGendersQuery` containing paging, filtering, and sorting parameters.
     - Retrieves the underlying `Gender` entity data from the repository.
     - Applies optional search filtering based on GenderName or Description.
     - Sorts and paginates the results according to the request parameters.
     - Maps entity objects to DTOs for returning to the client.
     - Returns both the total record count (for paging UI) and the filtered page results.

     Dependencies:
     - IRepository<Domain.Genders.Gender, Guid>:
         Provides access to the persistent storage of `Gender` entities.
     - ObjectMapper:
         Maps entity models (`Domain.Genders.Gender`) to data transfer models (`GenderDto`).
     - MediatR (IRequestHandler interface):
         Enables the class to handle requests and be integrated into a CQRS architecture.

     Usage Flow:
     1. Retrieve a queryable collection of Gender entities.
     2. Apply text filter if provided.
     3. Count the total filtered results.
     4. Apply sorting, skip, and take for pagination.
     5. Map entities to DTOs.
     6. Return the paged results wrapped in PagedResultDto.

     Notes:
     - Filtering is case-sensitive by default unless the database collation ignores case.
     - Sorting is dynamic, using the string-based property name from the request.
     - The repository is assumed to be async-enabled for efficient DB access.
 */
    public class GetGendersQueryHandler
        : ApplicationService,
          IRequestHandler<GetGendersQuery, PagedResultDto<GenderDto>>
    {
        // Repository for accessing Gender entities identified by Guid.
        private readonly IRepository<Gender, Guid> _genderRepository;

        // Constructor: injects the Gender repository dependency.
        public GetGendersQueryHandler(IRepository<Gender, Guid> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        // Handles the incoming query request.
        // request  : Contains input parameters for filtering, sorting, and pagination.
        // cancellationToken : Allows the operation to be cancelled (not used directly here).
        public async Task<PagedResultDto<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            // Get an IQueryable for Gender entities from the repository.
            var queryable = await _genderRepository.GetQueryableAsync();

            // Apply filter if the user provided a non-empty search string.
            if (!string.IsNullOrWhiteSpace(request.Input.Filter))
            {
                queryable = queryable.Where(x =>
                    x.GenderName.Contains(request.Input.Filter) ||    // Match gender name
                    (x.Description != null &&                         // Match description if present
                     x.Description.Contains(request.Input.Filter)));
            }

            // Count total results after filtering (needed for pagination metadata).
            var totalCount = queryable.Count();

            // Apply sorting (default to "GenderName" if none specified),
            // skip the requested number of records, and take only the page size.
            var genders = queryable
                .OrderBy(request.Input.Sorting ?? "GenderName")
                .Skip(request.Input.SkipCount)
                .Take(request.Input.MaxResultCount)
                .ToList();

            // Map the entity list to a list of GenderDto objects for client use.
            var genderDtos = ObjectMapper.Map<
                System.Collections.Generic.List<Gender>,
                System.Collections.Generic.List<GenderDto>>(genders);

            // Return the paged result including total count and the page's data.
            return new PagedResultDto<GenderDto>(totalCount, genderDtos);
        }
    }

}
