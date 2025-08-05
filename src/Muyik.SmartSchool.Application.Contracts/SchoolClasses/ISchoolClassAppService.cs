using System;
using System.Threading.Tasks;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Muyik.SmartSchool.SchoolClasses
{
    /// <summary>
    /// Defines the contract for school class-related application service operations.
    /// </summary>
    /// <remarks>
    /// This interface follows the application service pattern in the ABP framework and
    /// provides methods for CRUD operations on school classes.
    /// </remarks>
    public interface ISchoolClassAppService : IApplicationService
    {
        /// <summary>
        /// Retrieves the details of a specific school class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the school class.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the school class details.</returns>
        Task<SchoolClassDto> GetAsync(Guid id);

        /// <summary>
        /// Retrieves a paginated list of school classes based on the specified input.
        /// </summary>
        /// <param name="input">The pagination and filtering criteria.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a paginated list of school classes.
        /// </returns>
        Task<PagedResultDto<SchoolClassDto>> GetListAsync(GetSchoolClassesInput input);

        /// <summary>
        /// Creates a new school class with the specified details.
        /// </summary>
        /// <param name="input">The information of the school class to create.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the created school class details.
        /// </returns>
        Task<SchoolClassDto> CreateAsync(CreateSchoolClassDto input);

        /// <summary>
        /// Updates the specified school class with new information.
        /// </summary>
        /// <param name="id">The unique identifier of the school class to update.</param>
        /// <param name="input">The updated school class information.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the updated school class details.
        /// </returns>
        Task<SchoolClassDto> UpdateAsync(Guid id, UpdateSchoolClassDto input);

        /// <summary>
        /// Deletes the specified school class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the school class to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);
    }
}