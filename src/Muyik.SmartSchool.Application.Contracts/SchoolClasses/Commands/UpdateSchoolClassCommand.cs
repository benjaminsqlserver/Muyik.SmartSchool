using System;
using MediatR;
using Muyik.SmartSchool.SchoolClasses.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Commands
{
    /// <summary>
    /// Represents a command to update an existing school class.
    /// </summary>
    /// <remarks>
    /// This command encapsulates the request to update a school class and returns 
    /// the updated SchoolClassDto upon completion.
    /// </remarks>
    public class UpdateSchoolClassCommand : IRequest<SchoolClassDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the school class to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated school class data.
        /// </summary>
        public UpdateSchoolClassDto SchoolClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSchoolClassCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the school class to update.</param>
        /// <param name="schoolClass">The updated school class data.</param>
        public UpdateSchoolClassCommand(Guid id, UpdateSchoolClassDto schoolClass)
        {
            Id = id;
            SchoolClass = schoolClass;
        }
    }
}