// File: Muyik.SmartSchool.Application/Users/QueryHandlers/GetUsersQueryHandler.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
//using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
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
    /// Handles the <see cref="GetUsersQuery"/> to retrieve a paginated, filtered,
    /// and sorted list of users from the system.
    /// </summary>
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResultDto<UserDto>>
    {
        private readonly IIdentityUserRepository _userRepository;
        private readonly IRepository<Gender, Guid> _genderRepository;
        private readonly IRepository<SchoolClass, Guid> _schoolClassRepository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersQueryHandler"/> class.
        /// </summary>
        /// <param name="userRepository">Repository for accessing identity users.</param>
        /// <param name="genderRepository">Repository for retrieving gender information.</param>
        /// <param name="schoolClassRepository">Repository for retrieving school class information.</param>
        /// <param name="objectMapper">Object mapper for converting domain models to DTOs.</param>
        public GetUsersQueryHandler(
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
        /// Processes the <see cref="GetUsersQuery"/> request.
        /// Applies filters, sorting, and pagination to retrieve users and maps them to DTOs.
        /// </summary>
        /// <param name="request">The query containing filter and pagination criteria.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A <see cref="PagedResultDto{UserDto}"/> containing the total count and paged result of user DTOs.
        /// </returns>
        public async Task<PagedResultDto<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all users and cast to AppUser for custom fields
            var allUsers = await _userRepository.GetListAsync();
            var appUsers = allUsers.Cast<AppUser>().AsQueryable();

            // Apply text-based filter across multiple fields
            if (!string.IsNullOrWhiteSpace(request.Input.Filter))
            {
                var filter = request.Input.Filter.ToLower();
                appUsers = appUsers.Where(u =>
                    u.UserName.ToLower().Contains(filter) ||
                    u.Email.ToLower().Contains(filter) ||
                    (u.FirstName != null && u.FirstName.ToLower().Contains(filter)) ||
                    (u.MiddleName != null && u.MiddleName.ToLower().Contains(filter)));
            }

            // Apply optional gender filter
            if (request.Input.GenderId.HasValue)
            {
                appUsers = appUsers.Where(u => u.GenderId == request.Input.GenderId);
            }

            // Apply optional school class filter
            if (request.Input.SchoolClassId.HasValue)
            {
                appUsers = appUsers.Where(u => u.SchoolClassId == request.Input.SchoolClassId);
            }

            // Apply optional "HasLeftSchool" flag filter
            if (request.Input.HasLeftSchool.HasValue)
            {
                appUsers = appUsers.Where(u => u.HasLeftSchool == request.Input.HasLeftSchool);
            }

            var totalCount = appUsers.Count();

            // Apply sorting based on the provided field
            var sortProperty = request.Input.Sorting ?? "UserName";
            switch (sortProperty.ToLower())
            {
                case "username":
                    appUsers = appUsers.OrderBy(u => u.UserName);
                    break;
                case "email":
                    appUsers = appUsers.OrderBy(u => u.Email);
                    break;
                case "firstname":
                    appUsers = appUsers.OrderBy(u => u.FirstName);
                    break;
                case "creationtime":
                    appUsers = appUsers.OrderBy(u => u.CreationTime);
                    break;
                default:
                    appUsers = appUsers.OrderBy(u => u.UserName);
                    break;
            }

            // Apply paging
            var pagedUsers = appUsers
                .Skip(request.Input.SkipCount)
                .Take(request.Input.MaxResultCount)
                .ToList();

            // Convert to DTOs and enrich with related data
            var userDtos = new List<UserDto>();
            foreach (var user in pagedUsers)
            {
                userDtos.Add(await MapToUserDtoAsync(user));
            }

            return new PagedResultDto<UserDto>(totalCount, userDtos);
        }

        /// <summary>
        /// Maps an <see cref="AppUser"/> entity to a <see cref="UserDto"/>,
        /// including gender and school class names where applicable.
        /// </summary>
        /// <param name="user">The AppUser entity to map.</param>
        /// <returns>A mapped <see cref="UserDto"/> instance enriched with related information.</returns>
        private async Task<UserDto> MapToUserDtoAsync(AppUser user)
        {
            var userDto = _objectMapper.Map<AppUser, UserDto>(user);

            // Try to fetch and assign Gender name
            if (user.GenderId.HasValue)
            {
                try
                {
                    var gender = await _genderRepository.GetAsync(user.GenderId.Value);
                    userDto.GenderName = gender.GenderName;
                }
                catch
                {
                    userDto.GenderName = "Unknown"; // In case gender was deleted
                }
            }

            // Try to fetch and assign School Class name
            if (user.SchoolClassId.HasValue)
            {
                try
                {
                    var schoolClass = await _schoolClassRepository.GetAsync(user.SchoolClassId.Value);
                    userDto.SchoolClassName = schoolClass.ClassName;
                }
                catch
                {
                    userDto.SchoolClassName = "Unknown"; // In case class was deleted
                }
            }

            return userDto;
        }
    }
}
