using System;
using MediatR;
using Muyik.SmartSchool.Genders.Dtos;

namespace Muyik.SmartSchool.Genders.Queries
{
    /// <summary>
    /// Represents a query to retrieve a specific gender by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This query is handled by a MediatR handler which returns a GenderDto 
    /// containing the gender data.
    /// </remarks>
    public class GetGenderQuery : IRequest<GenderDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the gender to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGenderQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the gender.</param>
        public GetGenderQuery(Guid id)
        {
            Id = id;
        }
    }
}
