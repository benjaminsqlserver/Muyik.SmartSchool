// File: Muyik.SmartSchool.Application/Users/CommandHandlers/CreateUserCommandHandler.cs

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Guids;
using Muyik.SmartSchool.Users.Commands;
using Muyik.SmartSchool.Users.Dtos;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Users.CommandHandlers
{
    /// <summary>
    /// Handles the creation of a new user by processing the <see cref="CreateUserCommand"/>.
    /// </summary>
    /// <remarks>
    /// This command handler creates a new <see cref="AppUser"/> using the ABP Identity system,
    /// persists the user, and maps the result to a <see cref="UserDto"/> for output.
    /// </remarks>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IdentityUserManager _userManager;
        private readonly IRepository<Gender, Guid> _genderRepository;
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IGuidGenerator _guidGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userManager">The Identity user manager for managing user creation.</param>
        /// <param name="genderRepository">Repository to fetch related gender information.</param>
        /// <param name="schoolClassRepository">Repository to fetch related class information.</param>
        /// <param name="objectMapper">The object mapper to convert entities to DTOs.</param>
        /// <param name="guidGenerator">The GUID generator for creating new user IDs.</param>
        public CreateUserCommandHandler(
            IdentityUserManager userManager,
            IRepository<Gender, Guid> genderRepository,
            IRepository<SchoolClass, Guid> schoolClassRepository,
            IObjectMapper objectMapper,
            IGuidGenerator guidGenerator)
        {
            _userManager = userManager;
            _genderRepository = genderRepository;
            _schoolClassRepository = schoolClassRepository;
            _objectMapper = objectMapper;
            _guidGenerator = guidGenerator;
        }

        /// <summary>
        /// Handles the creation of a new <see cref="AppUser"/> using the provided request data.
        /// </summary>
        /// <param name="request">The command containing user input data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="UserDto"/> containing the newly created user's data.</returns>
        /// <exception cref="UserFriendlyException">Thrown if user creation fails due to validation errors.</exception>
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Create a new AppUser instance from the command input
            var user = new AppUser(
                _guidGenerator.Create(),
                request.User.UserName,
                request.User.Email,
                request.User.FirstName,
                request.User.MiddleName,
                request.User.DateOfBirth,
                request.User.UserPhoto,
                request.User.HasLeftSchool,
                request.User.Address,
                request.User.GenderId,
                request.User.SchoolClassId
            );

            // Attempt to create the user using ABP Identity
            var result = await _userManager.CreateAsync(user, request.User.Password);

            if (!result.Succeeded)
            {
                // Return a friendly error message if user creation fails
                throw new UserFriendlyException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Map user entity to a detailed DTO
            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Maps an <see cref="AppUser"/> entity to a <see cref="UserDto"/>, enriching the DTO with additional related data.
        /// </summary>
        /// <param name="user">The user entity to map.</param>
        /// <returns>The enriched <see cref="UserDto"/>.</returns>
        private async Task<UserDto> MapToUserDtoAsync(AppUser user)
        {
            var userDto = _objectMapper.Map<AppUser, UserDto>(user);

            // Populate related Gender name if available
            if (user.GenderId.HasValue)
            {
                var gender = await _genderRepository.GetAsync(user.GenderId.Value);
                userDto.GenderName = gender.GenderName;
            }

            // Populate related School Class name if available
            if (user.SchoolClassId.HasValue)
            {
                var schoolClass = await _schoolClassRepository.GetAsync(user.SchoolClassId.Value);
                userDto.SchoolClassName = schoolClass.ClassName;
            }

            return userDto;
        }
    }
}
