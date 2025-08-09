using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.SchoolClasses.Commands;
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
    /// Handles deleting a SchoolClass entity.
    /// </summary>
    public class DeleteSchoolClassCommandHandler
        : ApplicationService,
          IRequestHandler<DeleteSchoolClassCommand>
    {
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;

        public DeleteSchoolClassCommandHandler(IRepository<SchoolClass, Guid> schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task Handle(DeleteSchoolClassCommand request, CancellationToken cancellationToken)
        {
            // Directly delete entity by ID
            await _schoolClassRepository.DeleteAsync(request.Id);
        }
    }

}
