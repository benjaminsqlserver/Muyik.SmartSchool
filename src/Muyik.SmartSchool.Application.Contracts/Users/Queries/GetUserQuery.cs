// File: Muyik.SmartSchool.Application.Contracts/Users/Queries/GetUserQuery.cs

using System;
using MediatR;
using Muyik.SmartSchool.Users.Dtos;

namespace Muyik.SmartSchool.Users.Queries
{
    /// <summary>
    /// Represents a query to retrieve a specific user's details by their unique identifier.
    /// </summary>
    /// <remarks>
    /// This query is intended to be handled by a MediatR handler which returns a <see cref="UserDto"/>
    /// containing the user's data. It is part of the CQRS (Command Query Responsibility Segregation) pattern,
    /// separating data retrieval logic from other concerns like commands or updates.
    /// </remarks>
    public class GetUserQuery : IRequest<UserDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserQuery"/> class with the specified user ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
