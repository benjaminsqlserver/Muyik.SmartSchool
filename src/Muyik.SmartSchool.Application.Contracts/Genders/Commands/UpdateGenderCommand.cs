using System;
using MediatR;
using Muyik.SmartSchool.Genders.Dtos;

namespace Muyik.SmartSchool.Genders.Commands
{
    /// <summary>
    /// Represents a command to update an existing gender.
    /// </summary>
    /// <remarks>
    /// This command encapsulates the request to update a gender and returns 
    /// the updated GenderDto upon completion.
    /// </remarks>
    public class UpdateGenderCommand : IRequest<GenderDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the gender to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the updated gender data.
        /// </summary>
        public UpdateGenderDto Gender { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGenderCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the gender to update.</param>
        /// <param name="gender">The updated gender data.</param>
        public UpdateGenderCommand(Guid id, UpdateGenderDto gender)
        {
            Id = id;
            Gender = gender;
        }
    }
}