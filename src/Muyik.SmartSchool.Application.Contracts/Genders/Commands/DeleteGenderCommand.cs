using System;
using MediatR;

namespace Muyik.SmartSchool.Genders.Commands
{
    /// <summary>
    /// Represents a command to delete a gender from the system.
    /// </summary>
    /// <remarks>
    /// This command is typically handled by a MediatR handler that performs the actual deletion 
    /// logic, such as removing the gender from the database.
    /// </remarks>
    public class DeleteGenderCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the gender to be deleted.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteGenderCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the gender to be deleted.</param>
        public DeleteGenderCommand(Guid id)
        {
            Id = id;
        }
    }
}