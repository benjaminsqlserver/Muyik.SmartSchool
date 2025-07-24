// File: Muyik.SmartSchool.Application.Contracts/Users/Commands/UpdateUserCommand.cs

// Importing required namespaces
using System; // Provides access to base .NET types, including Guid.
using MediatR; // Used for implementing the mediator pattern for decoupled request/response handling.
using Muyik.SmartSchool.Users.Dtos; // Contains DTOs related to user entities.

namespace Muyik.SmartSchool.Users.Commands
{
    // The UpdateUserCommand class represents a request to update an existing user.
    // It implements IRequest<UserDto>, which means this command will return a UserDto upon completion.
    public class UpdateUserCommand : IRequest<UserDto>
    {
        // The unique identifier of the user to be updated.
        public Guid Id { get; set; }

        // DTO containing the new user data to apply.
        // This  includes updatable fields such as Name, Email, Role, etc.
        public UpdateUserDto User { get; set; }

        // Constructor that initializes the command with the user's ID and the updated data.
        public UpdateUserCommand(Guid id, UpdateUserDto user)
        {
            Id = id;
            User = user;
        }
    }
}
