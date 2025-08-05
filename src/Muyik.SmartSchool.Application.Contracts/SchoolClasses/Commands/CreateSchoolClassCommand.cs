using MediatR;
using Muyik.SmartSchool.SchoolClasses.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Commands
{
    /// <summary>
    /// Represents a command to create a new school class.
    /// </summary>
    /// <remarks>
    /// This command encapsulates the request to create a new school class and is handled 
    /// by a MediatR handler that performs the actual creation logic.
    /// </remarks>
    public class CreateSchoolClassCommand : IRequest<SchoolClassDto>
    {
        /// <summary>
        /// Gets or sets the data needed to create a new school class.
        /// </summary>
        public CreateSchoolClassDto SchoolClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSchoolClassCommand"/> class.
        /// </summary>
        /// <param name="schoolClass">The school class data to create.</param>
        public CreateSchoolClassCommand(CreateSchoolClassDto schoolClass)
        {
            SchoolClass = schoolClass;
        }
    }
}