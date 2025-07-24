// Importing necessary namespaces
using MediatR; // MediatR is a popular .NET library that implements the Mediator pattern. 
               // It allows for loosely coupled communication between components.

using Muyik.SmartSchool.Users.Dtos; // This namespace contains Data Transfer Objects (DTOs) 
                                    // used for user-related operations within the application.

namespace Muyik.SmartSchool.Users.Commands
{
    // Define a command class named CreateUserCommand that implements the IRequest interface
    // from MediatR. This command is used to encapsulate the request to create a new user.
    // The generic parameter <UserDto> indicates the expected return type after handling this request.
    public class CreateUserCommand : IRequest<UserDto>
    {
        // Property to hold the data needed to create a new user.
        // CreateUserDto includes fields such as Name, Email, Password, etc.
        public CreateUserDto User { get; set; }

        // Constructor that initializes the CreateUserCommand with the provided CreateUserDto object.
        // This allows the command to carry the necessary data to the handler that will process it.
        public CreateUserCommand(CreateUserDto user)
        {
            User = user;
        }
    }
}
