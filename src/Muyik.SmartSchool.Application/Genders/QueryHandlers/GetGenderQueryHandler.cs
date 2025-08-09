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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Muyik.SmartSchool.Genders.QueryHandlers
{
    public class GetGenderQueryHandler
        : ApplicationService,
          IRequestHandler<GetGenderQuery, GenderDto>
    {
        private readonly IRepository<Gender, Guid> _genderRepository;

        public GetGenderQueryHandler(IRepository<Gender, Guid> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            var gender = await _genderRepository.GetAsync(request.Id);
            return ObjectMapper.Map<Gender, GenderDto>(gender);
        }
    }


}
