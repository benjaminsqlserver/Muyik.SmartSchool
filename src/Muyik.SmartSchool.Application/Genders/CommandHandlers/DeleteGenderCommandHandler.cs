using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Genders.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Muyik.SmartSchool.Genders.CommandHandlers
{
    public class DeleteGenderCommandHandler: ApplicationService, IRequestHandler<DeleteGenderCommand>
    {
        private readonly IRepository<Gender, Guid> _genderRepository;

        public DeleteGenderCommandHandler(IRepository<Gender, Guid> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            await _genderRepository.DeleteAsync(request.Id);
        }
    }

}
