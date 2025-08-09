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

namespace Muyik.SmartSchool.SchoolClasses.CommandHandlers
{
    /// <summary>
    /// Handles updating an existing SchoolClass entity.
    /// </summary>
    public class UpdateSchoolClassCommandHandler
        : ApplicationService,
          IRequestHandler<UpdateSchoolClassCommand, SchoolClassDto>
    {
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;

        public UpdateSchoolClassCommandHandler(IRepository<SchoolClass, Guid> schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<SchoolClassDto> Handle(UpdateSchoolClassCommand request, CancellationToken cancellationToken)
        {
            // Retrieve existing entity by ID
            var schoolClass = await _schoolClassRepository.GetAsync(request.Id);

            // Apply business rules through entity setters
            schoolClass.SetClassName(request.SchoolClass.ClassName);
            schoolClass.SetDescription(request.SchoolClass.Description);

            // Save changes to database
            await _schoolClassRepository.UpdateAsync(schoolClass, autoSave: true);

            return ObjectMapper.Map<SchoolClass, SchoolClassDto>(schoolClass);
        }
    }

}
