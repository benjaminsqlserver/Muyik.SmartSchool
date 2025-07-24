// File: Muyik.SmartSchool.Application/Users/CommandHandlers/DeleteUserCommandHandler.cs

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Volo.Abp.Identity;
using Muyik.SmartSchool.Users.Commands;

namespace Muyik.SmartSchool.Users.CommandHandlers
{
    /// <summary>
    /// Handles the deletion of a user in the SmartSchool application.
    /// This class implements the MediatR <see cref="IRequestHandler{TRequest}"/> interface
    /// to respond to <see cref="DeleteUserCommand"/> requests.
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IIdentityUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">
        /// An implementation of <see cref="IIdentityUserRepository"/> used to access and manage identity users.
        /// </param>
        public DeleteUserCommandHandler(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the <see cref="DeleteUserCommand"/> by deleting the specified user from the system.
        /// </summary>
        /// <param name="request">The command containing the ID of the user to delete.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Calls the user repository to delete the user by ID with autoSave enabled.
            await _userRepository.DeleteAsync(request.Id, autoSave: true, cancellationToken: cancellationToken);
        }
    }
}
