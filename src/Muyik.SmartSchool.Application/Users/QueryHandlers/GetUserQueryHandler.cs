// File: Muyik.SmartSchool.Application/Users/QueryHandlers/GetUserQueryHandler.cs

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
//using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Muyik.SmartSchool.Users.Queries;
using Muyik.SmartSchool.Users.Dtos;
using Muyik.SmartSchool.Users;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Users.QueryHandlers
{
    /// <summary>
    /// Handles the retrieval of a user's full information by their unique identifier.
    /// Implements the MediatR <see cref="IRequestHandler{TRequest,TResponse}"/> interface
    /// to process <see cref="GetUserQuery"/> and return a <see cref="UserDto"/>.
    /// </summary>
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IIdentityUserRepository _userRepository;
        private readonly IRepository<Gender, Guid> _genderRepository;
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserQueryHandler"/> class.
        /// </summary>
        /// <param name="userRepository">Repository for managing identity users.</param>
        /// <param name="genderRepository">Repository for accessing gender information.</param>
        /// <param name="schoolClassRepository">Repository for accessing school class information.</param>
        /// <param name="objectMapper">Object mapper for converting domain entities to DTOs.</param>
        public GetUserQueryHandler(
            IIdentityUserRepository userRepository,
            IRepository<Gender, Guid> genderRepository,
            IRepository<SchoolClass, Guid> schoolClassRepository,
            IObjectMapper objectMapper)
        {
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _schoolClassRepository = schoolClassRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// Handles the request to retrieve a user by ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the user to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token for cooperative cancellation.</param>
        /// <returns>A <see cref="UserDto"/> containing user information enriched with gender and class data.</returns>
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id) as AppUser;
            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Maps an <see cref="AppUser"/> entity to a <see cref="UserDto"/> and enriches it
        /// with related data such as gender name and school class name.
        /// </summary>
        /// <param name="user">The domain user entity to map.</param>
        /// <returns>A fully populated <see cref="UserDto"/> object.</returns>
        private async Task<UserDto> MapToUserDtoAsync(AppUser user)
        {
            var userDto = _objectMapper.Map<AppUser, UserDto>(user);

            if (user.GenderId.HasValue)
            {
                var gender = await _genderRepository.GetAsync(user.GenderId.Value);
                userDto.GenderName = gender.GenderName;
            }

            if (user.SchoolClassId.HasValue)
            {
                var schoolClass = await _schoolClassRepository.GetAsync(user.SchoolClassId.Value);
                userDto.SchoolClassName = schoolClass.ClassName;
            }

            return userDto;
        }
    }
}
