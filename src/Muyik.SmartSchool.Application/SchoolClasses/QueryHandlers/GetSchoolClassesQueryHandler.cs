using AutoMapper.Internal.Mappers;
using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using Muyik.SmartSchool.SchoolClasses.Queries;
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


namespace Muyik.SmartSchool.SchoolClasses.QueryHandlers
{
    /// <summary>
    /// Handles paginated retrieval of multiple SchoolClass entities.
    /// </summary>
    public class GetSchoolClassesQueryHandler
        : ApplicationService,
          IRequestHandler<GetSchoolClassesQuery, PagedResultDto<SchoolClassDto>>
    {
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;

        public GetSchoolClassesQueryHandler(IRepository<SchoolClass, Guid> schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<PagedResultDto<SchoolClassDto>> Handle(GetSchoolClassesQuery request, CancellationToken cancellationToken)
        {
            var queryable = await _schoolClassRepository.GetQueryableAsync();

            // Apply filtering if provided
            if (!string.IsNullOrWhiteSpace(request.Input.Filter))
            {
                queryable = queryable.Where(x =>
                    x.ClassName.Contains(request.Input.Filter) ||
                    (x.Description != null && x.Description.Contains(request.Input.Filter)));
            }

            var totalCount = queryable.Count();

            // Apply sorting and pagination
            var schoolClasses = queryable
                .OrderBy(request.Input.Sorting ?? "ClassName") // Uses Dynamic LINQ
                .Skip(request.Input.SkipCount)
                .Take(request.Input.MaxResultCount)
                .ToList();

            // Map entity list to DTO list
            var schoolClassDtos = ObjectMapper.Map<
                System.Collections.Generic.List<SchoolClass>,
                System.Collections.Generic.List<SchoolClassDto>>(schoolClasses);

            return new PagedResultDto<SchoolClassDto>(totalCount, schoolClassDtos);
        }
    }
}

