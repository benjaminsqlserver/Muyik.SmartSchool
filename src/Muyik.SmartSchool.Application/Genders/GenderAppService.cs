using MediatR;
using Muyik.SmartSchool.Genders.Commands;
using Muyik.SmartSchool.Genders.Dtos;
using Muyik.SmartSchool.Genders.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

/*
    GenderAppService
    ----------------
    This application service acts as a façade for all "Gender" entity operations 
    within the SmartSchool domain, implementing the `IGenderAppService` interface. 
    It provides CRUD-style methods for working with genders, delegating actual 
    business logic and data handling to MediatR request handlers via the IMediator.

    Key Responsibilities:
    - Receive high-level application requests for Gender data and operations.
    - Translate these into MediatR queries or commands and dispatch them.
    - Return DTOs or paginated results to the caller (UI layer, API controller, etc.).
    - Keep the service layer thin by avoiding direct business logic implementation.

    Dependencies:
    - IMediator:
        Central messaging hub from the MediatR library. Decouples the service 
        from the underlying query/command handlers in a CQRS pattern.
    - DTO classes:
        Used to send and receive structured data without exposing domain entities.

    Design Notes:
    - Follows the CQRS (Command Query Responsibility Segregation) pattern.
    - Uses asynchronous programming for all operations to improve scalability.
    - The service itself does not validate or transform data — that is expected 
      in the command/query handlers or the domain layer.
    - Commands are used for create/update/delete operations; queries for reads.

    Provided Methods:
    - GetAsync(id)           : Retrieves a single GenderDto by its Guid.
    - GetListAsync(input)    : Retrieves a paginated, optionally filtered list of GenderDto.
    - CreateAsync(input)     : Creates a new gender record.
    - UpdateAsync(id, input) : Updates an existing gender record.
    - DeleteAsync(id)        : Deletes an existing gender record.
*/
namespace Muyik.SmartSchool.Genders
{
    public class GenderAppService : ApplicationService, IGenderAppService
    {
        // MediatR mediator used to dispatch commands/queries to their handlers.
        private readonly IMediator _mediator;

        // Constructor: injects the mediator dependency.
        public GenderAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Retrieves a single gender record by its unique identifier.
        public async Task<GenderDto> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetGenderQuery(id));
        }

        // Retrieves a paginated list of genders based on input filters and paging info.
        public async Task<PagedResultDto<GenderDto>> GetListAsync(GetGendersInput input)
        {
            return await _mediator.Send(new GetGendersQuery(input));
        }

        // Creates a new gender record using the provided input data.
        public async Task<GenderDto> CreateAsync(CreateGenderDto input)
        {
            return await _mediator.Send(new CreateGenderCommand(input));
        }

        // Updates an existing gender record identified by `id` with the given input data.
        public async Task<GenderDto> UpdateAsync(Guid id, UpdateGenderDto input)
        {
            return await _mediator.Send(new UpdateGenderCommand(id, input));
        }

        // Deletes an existing gender record identified by `id`.
        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteGenderCommand(id));
        }
    }
}

