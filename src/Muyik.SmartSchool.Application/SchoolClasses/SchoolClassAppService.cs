using MediatR;
using Muyik.SmartSchool.SchoolClasses.Commands;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using Muyik.SmartSchool.SchoolClasses.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Muyik.SmartSchool.SchoolClasses
{
    /// <summary>
    /// Application service for SchoolClass operations.
    /// Uses IMediator to send commands/queries to handlers.
    /// </summary>
    public class SchoolClassAppService : ApplicationService, ISchoolClassAppService
    {
        private readonly IMediator _mediator;

        public SchoolClassAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SchoolClassDto> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetSchoolClassQuery(id));
        }

        public async Task<PagedResultDto<SchoolClassDto>> GetListAsync(GetSchoolClassesInput input)
        {
            return await _mediator.Send(new GetSchoolClassesQuery(input));
        }

        public async Task<SchoolClassDto> CreateAsync(CreateSchoolClassDto input)
        {
            return await _mediator.Send(new CreateSchoolClassCommand(input));
        }

        public async Task<SchoolClassDto> UpdateAsync(Guid id, UpdateSchoolClassDto input)
        {
            return await _mediator.Send(new UpdateSchoolClassCommand(id, input));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteSchoolClassCommand(id));
        }
    }

}
