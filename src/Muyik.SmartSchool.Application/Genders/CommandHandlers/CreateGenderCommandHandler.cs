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
using Volo.Abp.Guids;

namespace Muyik.SmartSchool.Genders.CommandHandlers
{
    public class CreateGenderCommandHandler
         : ApplicationService,
           IRequestHandler<CreateGenderCommand, GenderDto>
    {
        private readonly IRepository<Gender, Guid> _genderRepository;

        public CreateGenderCommandHandler(IRepository<Gender, Guid> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var gender = new Gender(
                GuidGenerator.Create(),
                request.Gender.GenderName,
                request.Gender.Description
            );

            await _genderRepository.InsertAsync(gender, autoSave: true);
            return ObjectMapper.Map<Gender, GenderDto>(gender);
        }

    }
}