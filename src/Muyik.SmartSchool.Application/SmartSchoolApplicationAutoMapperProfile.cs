// File: Muyik.SmartSchool.Application/SmartSchoolApplicationAutoMapperProfile.cs

using AutoMapper;
using Muyik.SmartSchool.Entities;
using Muyik.SmartSchool.Genders.Dtos;
using Muyik.SmartSchool.SchoolClasses.Dtos;
using Muyik.SmartSchool.Users;
using Muyik.SmartSchool.Users.Dtos;

namespace Muyik.SmartSchool
{
    /// <summary>
    /// Defines AutoMapper mappings for the SmartSchool application layer.
    /// This profile maps between domain entities and Data Transfer Objects (DTOs)
    /// used in user-related operations.
    /// </summary>
    public class SmartSchoolApplicationAutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSchoolApplicationAutoMapperProfile"/> class
        /// and configures the object-object mappings used throughout the application layer.
        /// </summary>
        public SmartSchoolApplicationAutoMapperProfile()
        {
            // Maps AppUser entity to UserDto for reading user data
            CreateMap<AppUser, UserDto>();

            // Maps CreateUserDto to AppUser entity for creating new users
            CreateMap<CreateUserDto, AppUser>();

            // Maps UpdateUserDto to AppUser entity for updating existing users
            CreateMap<UpdateUserDto, AppUser>();


            // SchoolClass mappings
            CreateMap<SchoolClass, SchoolClassDto>();
            CreateMap<CreateSchoolClassDto, SchoolClass>();
            CreateMap<UpdateSchoolClassDto, SchoolClass>();

            // Gender mappings
            CreateMap<Gender, GenderDto>();
            CreateMap<CreateGenderDto, Gender>();
            CreateMap<UpdateGenderDto, Gender>();

        }
    }
}