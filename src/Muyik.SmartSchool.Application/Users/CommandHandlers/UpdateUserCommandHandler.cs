// File: Muyik.SmartSchool.Application/Users/CommandHandlers/UpdateUserCommandHandler.cs

using MediatR;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Users.Commands;
using Muyik.SmartSchool.Users.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Muyik.SmartSchool.Users.CommandHandlers
{
    /// <summary>
    /// Handles updating an existing <see cref="AppUser"/> by processing the <see cref="UpdateUserCommand"/>.
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IIdentityUserRepository _userRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IRepository<Gender, Guid> _genderRepository;
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
        /// </summary>
        public UpdateUserCommandHandler(
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager,
            IRepository<Gender, Guid> genderRepository,
            IRepository<SchoolClass, Guid> schoolClassRepository,
            IObjectMapper objectMapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _genderRepository = genderRepository;
            _schoolClassRepository = schoolClassRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// Handles the update of user data, including both Identity properties and custom fields.
        /// </summary>
        /// <param name="request">The command containing user update information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated <see cref="UserDto"/> with related properties populated.</returns>
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Retrieve and cast user to domain-specific AppUser
            var user = await _userRepository.GetAsync(request.Id) as AppUser;

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(AppUser), request.Id);
            }

            // Update Identity-managed properties (username/email)
            await _userManager.SetUserNameAsync(user, request.User.UserName);
            await _userManager.SetEmailAsync(user, request.User.Email);

            // Update domain-specific user properties
            user.FirstName = request.User.FirstName;
            user.MiddleName = request.User.MiddleName;
            user.DateOfBirth = request.User.DateOfBirth;
            user.UserPhoto = request.User.UserPhoto;
            user.HasLeftSchool = request.User.HasLeftSchool;
            user.Address = request.User.Address;
            user.GenderId = request.User.GenderId;
            user.SchoolClassId = request.User.SchoolClassId;

            // Save changes
            await _userManager.UpdateAsync(user);

            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Maps an <see cref="AppUser"/> to a <see cref="UserDto"/>, populating related fields.
        /// </summary>
        /// <param name="user">The updated user entity.</param>
        /// <returns><see cref="UserDto"/> with enriched values for display.</returns>
        private async Task<UserDto> MapToUserDtoAsync(AppUser user)
        {
            var userDto = _objectMapper.Map<AppUser, UserDto>(user);

            // Add Gender name if available
            if (user.GenderId.HasValue)
            {
                var gender = await _genderRepository.GetAsync(user.GenderId.Value);
                userDto.GenderName = gender.GenderName;
            }

            // Add SchoolClass name if available
            if (user.SchoolClassId.HasValue)
            {
                var schoolClass = await _schoolClassRepository.GetAsync(user.SchoolClassId.Value);
                userDto.SchoolClassName = schoolClass.ClassName;
            }

            return userDto;
        }
    }
}
