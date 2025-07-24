// File: Muyik.SmartSchool.Application/Users/UserAppService.cs

using System;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp.Application.Services;
using Muyik.SmartSchool.Users.Commands;
using Muyik.SmartSchool.Users.Queries;
using Muyik.SmartSchool.Users.Dtos;
using Volo.Abp.Application.Dtos;

namespace Muyik.SmartSchool.Users
{
    /// <summary>
    /// Application service that acts as a façade over user-related operations.
    /// This service delegates user CRUD functionality to corresponding MediatR commands and queries.
    /// </summary>
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAppService"/> class.
        /// </summary>
        /// <param name="mediator">MediatR instance used to send commands and queries.</param>
        public UserAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>A <see cref="UserDto"/> representing the user.</returns>
        public async Task<UserDto> GetAsync(Guid id)
        {
            return await _mediator.Send(new GetUserQuery(id));
        }

        /// <summary>
        /// Retrieves a paginated and optionally filtered list of users.
        /// </summary>
        /// <param name="input">Input criteria for filtering, sorting, and paging.</param>
        /// <returns>
        /// A <see cref="PagedResultDto{UserDto}"/> containing the total count and the list of users.
        /// </returns>
        public async Task<PagedResultDto<UserDto>> GetListAsync(GetUsersInput input)
        {
            return await _mediator.Send(new GetUsersQuery(input));
        }

        /// <summary>
        /// Creates a new user with the provided details.
        /// </summary>
        /// <param name="input">The user data to create.</param>
        /// <returns>The created <see cref="UserDto"/>.</returns>
        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            return await _mediator.Send(new CreateUserCommand(input));
        }

        /// <summary>
        /// Updates an existing user with the given ID and new information.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="input">The updated user information.</param>
        /// <returns>The updated <see cref="UserDto"/>.</returns>
        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input)
        {
            return await _mediator.Send(new UpdateUserCommand(id, input));
        }

        /// <summary>
        /// Deletes the user associated with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
        }
    }
}
