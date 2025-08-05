using System;
using MediatR;

namespace Muyik.SmartSchool.SchoolClasses.Commands
{
    /// <summary>
    /// Represents a command to delete a school class from the system.
    /// </summary>
    /// <remarks>
    /// This command is typically handled by a MediatR handler that performs the actual deletion 
    /// logic, such as removing the school class from the database.
    /// </remarks>
    public class DeleteSchoolClassCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the school class to be deleted.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSchoolClassCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the school class to be deleted.</param>
        public DeleteSchoolClassCommand(Guid id)
        {
            Id = id;
        }
    }
}