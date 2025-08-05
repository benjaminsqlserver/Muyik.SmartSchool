using System;
using MediatR;
using Muyik.SmartSchool.SchoolClasses.Dtos;

namespace Muyik.SmartSchool.SchoolClasses.Queries
{
    /// <summary>
    /// Represents a query to retrieve a specific school class by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This query is handled by a MediatR handler which returns a SchoolClassDto 
    /// containing the school class data.
    /// </remarks>
    public class GetSchoolClassQuery : IRequest<SchoolClassDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the school class to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSchoolClassQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the school class.</param>
        public GetSchoolClassQuery(Guid id)
        {
            Id = id;
        }
    }
}