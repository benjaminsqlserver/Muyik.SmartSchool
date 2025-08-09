using AutoMapper.Internal.Mappers;
using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Genders.Commands;
using Muyik.SmartSchool.Genders.Dtos;
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
    public class UpdateGenderCommandHandler
        : ApplicationService,
          IRequestHandler<UpdateGenderCommand, GenderDto>
    {
        private readonly IRepository<Gender, Guid> _genderRepository;

        public UpdateGenderCommandHandler(IRepository<Gender, Guid> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = await _genderRepository.GetAsync(request.Id);
            
            await _genderRepository.UpdateAsync(gender, autoSave: true);
            return ObjectMapper.Map<Gender, GenderDto>(gender);
        }
    }

}
