// File: Muyik.SmartSchool.Application.Contracts/Users/Commands/DeleteUserCommand.cs

using System;
using MediatR;

namespace Muyik.SmartSchool.Users.Commands
{
    /// <summary>
    /// Represents a command to delete a user from the system.
    /// </summary>
    /// <remarks>
    /// This command is typically handled by a MediatR handler that performs the actual deletion 
    /// logic, such as removing the user from a database. It encapsulates only the information 
    /// required to identify the user to be deleted.
    /// </remarks>
    public class DeleteUserCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to be deleted.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class with the specified user ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to be deleted.</param>
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
