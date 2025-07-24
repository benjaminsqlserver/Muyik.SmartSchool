using System;
using System.Threading.Tasks;
using Muyik.SmartSchool.Users.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Muyik.SmartSchool.Users
{
    /// <summary>
    /// Defines the contract for user-related application service operations.
    /// </summary>
    /// <remarks>
    /// This interface follows the application service pattern in the ABP framework and
    /// provides methods for CRUD operations on users.
    /// </remarks>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// Retrieves the details of a specific user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user details.</returns>
        Task<UserDto> GetAsync(Guid id);

        /// <summary>
        /// Retrieves a paginated list of users based on the specified input.
        /// </summary>
        /// <param name="input">The pagination and filtering criteria.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a paginated list of users.
        /// </returns>
        Task<PagedResultDto<UserDto>> GetListAsync(GetUsersInput input);

        /// <summary>
        /// Creates a new user with the specified details.
        /// </summary>
        /// <param name="input">The information of the user to create.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the created user's details.
        /// </returns>
        Task<UserDto> CreateAsync(CreateUserDto input);

        /// <summary>
        /// Updates the specified user with new information.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="input">The updated user information.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated user's details.
        /// </returns>
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input);

        /// <summary>
        /// Deletes the specified user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);
    }
}
