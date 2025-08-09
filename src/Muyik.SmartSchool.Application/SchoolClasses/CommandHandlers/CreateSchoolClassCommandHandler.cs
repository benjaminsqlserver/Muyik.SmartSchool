using AutoMapper.Internal.Mappers;
using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.SchoolClasses.Commands;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Muyik.SmartSchool.SchoolClasses.CommandHandlers
{
    /// <summary>
    /// Handles creation of a new SchoolClass entity.
    /// </summary>
    public class CreateSchoolClassCommandHandler
        : ApplicationService,
          IRequestHandler<CreateSchoolClassCommand, SchoolClassDto>
    {
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;

        public CreateSchoolClassCommandHandler(IRepository<SchoolClass, Guid> schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDto> Handle(CreateSchoolClassCommand request, CancellationToken cancellationToken)
        {
            // Create a new domain entity using ABP's GUID generator
            var schoolClass = new SchoolClass(
                GuidGenerator.Create(),
                request.SchoolClass.ClassName,
                request.SchoolClass.Description
            );

            // Save to database and commit immediately
            await _schoolClassRepository.InsertAsync(schoolClass, autoSave: true);

            // Convert domain entity to DTO for returning to the client
            return ObjectMapper.Map<SchoolClass, SchoolClassDto>(schoolClass);
        }
    }

}
