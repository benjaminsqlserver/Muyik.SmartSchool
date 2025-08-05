using MediatR;
using Muyik.SmartSchool.Genders.Dtos;

namespace Muyik.SmartSchool.Genders.Commands
{
    /// <summary>
    /// Represents a command to create a new gender.
    /// </summary>
    /// <remarks>
    /// This command encapsulates the request to create a new gender and is handled 
    /// by a MediatR handler that performs the actual creation logic.
    /// </remarks>
    public class CreateGenderCommand : IRequest<GenderDto>
    {
        /// <summary>
        /// Gets or sets the data needed to create a new gender.
        /// </summary>
        public CreateGenderDto Gender { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGenderCommand"/> class.
        /// </summary>
        /// <param name="gender">The gender data to create.</param>
        public CreateGenderCommand(CreateGenderDto gender)
        {
            Gender = gender;
        }
    }
}