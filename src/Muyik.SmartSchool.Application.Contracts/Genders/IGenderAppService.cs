using System;
using System.Threading.Tasks;
using Muyik.SmartSchool.Genders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Muyik.SmartSchool.Genders
{
    /// <summary>
    /// Defines the contract for gender-related application service operations.
    /// </summary>
    /// <remarks>
    /// This interface follows the application service pattern in the ABP framework and
    /// provides methods for CRUD operations on genders.
    /// </remarks>
    public interface IGenderAppService : IApplicationService
    {
        /// <summary>
        /// Retrieves the details of a specific gender by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the gender.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the gender details.</returns>
        Task<GenderDto> GetAsync(Guid id);

        /// <summary>
        /// Retrieves a paginated list of genders based on the specified input.
        /// </summary>
        /// <param name="input">The pagination and filtering criteria.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a paginated list of genders.
        /// </returns>
        Task<PagedResultDto<GenderDto>> GetListAsync(GetGendersInput input);

        /// <summary>
        /// Creates a new gender with the specified details.
        /// </summary>
        /// <param name="input">The information of the gender to create.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the created gender details.
        /// </returns>
        Task<GenderDto> CreateAsync(CreateGenderDto input);

        /// <summary>
        /// Updates the specified gender with new information.
        /// </summary>
        /// <param name="id">The unique identifier of the gender to update.</param>
        /// <param name="input">The updated gender information.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated gender details.
        /// </returns>
        Task<GenderDto> UpdateAsync(Guid id, UpdateGenderDto input);

        /// <summary>
        /// Deletes the specified gender by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the gender to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);
    }
}