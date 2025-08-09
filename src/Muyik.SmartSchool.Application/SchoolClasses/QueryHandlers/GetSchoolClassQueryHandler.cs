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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Muyik.SmartSchool.SchoolClasses.QueryHandlers
{
    /// <summary>
    /// Handles retrieval of a single SchoolClass entity by ID.
    /// </summary>
    public class GetSchoolClassQueryHandler
        : ApplicationService,
          IRequestHandler<GetSchoolClassQuery, SchoolClassDto>
    {
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;

        public GetSchoolClassQueryHandler(IRepository<SchoolClass, Guid> schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDto> Handle(GetSchoolClassQuery request, CancellationToken cancellationToken)
        {
            var schoolClass = await _schoolClassRepository.GetAsync(request.Id);
            return ObjectMapper.Map<SchoolClass, SchoolClassDto>(schoolClass);
        }

    }
}