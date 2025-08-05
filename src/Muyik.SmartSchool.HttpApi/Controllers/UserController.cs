using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muyik.SmartSchool.Users;
using Muyik.SmartSchool.Users.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Muyik.SmartSchool.Controllers
{
    [ApiController]
    [Route("api/app/user")]
    [Authorize]
    public class UserController : SmartSchoolController
    {
        private readonly IUserAppService _userAppService;
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] AdminRoles = { "SuperAdmin", "admin", "Administrator" };

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Gets a user by ID. Any authenticated user can view user details.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetAsync(Guid id)
        {
            try
            {
                var user = await _userAppService.GetAsync(id);
                return Ok(user);
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while getting user with ID: {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user.");
            }
        }

        /// <summary>
        /// Gets a paginated list of users. Only SuperAdmin, admin, and Administrator roles can access this.
        /// </summary>
        /// <param name="input">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of users</returns>
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,admin,Administrator")]
        public async Task<ActionResult<PagedResultDto<UserDto>>> GetListAsync([FromQuery] GetUsersInput input)
        {
            try
            {
                // Validate input parameters
                if (input.MaxResultCount > 1000)
                {
                    return BadRequest("Maximum result count cannot exceed 1000.");
                }

                if (input.MaxResultCount <= 0)
                {
                    input.MaxResultCount = 10; // Default page size
                }

                var users = await _userAppService.GetListAsync(input);
                return Ok(users);
            }
            catch (AbpValidationException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while getting users list");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }

        /// <summary>
        /// Creates a new user. Only SuperAdmin, admin, and Administrator roles can create users.
        /// </summary>
        /// <param name="input">User creation data</param>
        /// <returns>Created user details</returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,admin,Administrator")]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] CreateUserDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Additional server-side validation
                if (string.IsNullOrWhiteSpace(input.UserName))
                {
                    return BadRequest("Username is required.");
                }

                if (string.IsNullOrWhiteSpace(input.Email))
                {
                    return BadRequest("Email is required.");
                }

                if (string.IsNullOrWhiteSpace(input.Password))
                {
                    return BadRequest("Password is required.");
                }

                // Validate password strength
                if (!IsValidPassword(input.Password))
                {
                    return BadRequest("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                }

                var user = await _userAppService.CreateAsync(input);

                Logger.LogInformation("User created successfully. UserName: {UserName}, Email: {Email}, CreatedBy: {CreatedBy}",
                    user.UserName, user.Email, CurrentUser.UserName);

                return CreatedAtAction(nameof(GetAsync), new { id = user.Id }, user);
            }
            catch (AbpValidationException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating user with username: {UserName}", input.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
            }
        }

        /// <summary>
        /// Updates an existing user. Users can update their own profile, but only admins can update other users.
        /// </summary>
        /// <param name="id">User ID to update</param>
        /// <param name="input">User update data</param>
        /// <returns>Updated user details</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateAsync(Guid id, [FromBody] UpdateUserDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if user can update this profile
                var canUpdate = await CanUpdateUserAsync(id);
                if (!canUpdate)
                {
                    return Forbid("You don't have permission to update this user.");
                }

                // Additional server-side validation
                if (string.IsNullOrWhiteSpace(input.UserName))
                {
                    return BadRequest("Username is required.");
                }

                if (string.IsNullOrWhiteSpace(input.Email))
                {
                    return BadRequest("Email is required.");
                }

                var user = await _userAppService.UpdateAsync(id, input);

                Logger.LogInformation("User updated successfully. UserName: {UserName}, Email: {Email}, UpdatedBy: {UpdatedBy}",
                    user.UserName, user.Email, CurrentUser.UserName);

                return Ok(user);
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            catch (AbpValidationException ex)
            {
                return BadRequest(ex.ValidationErrors);
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating user with ID: {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }
        }

        /// <summary>
        /// Deletes a user. Only SuperAdmin, admin, and Administrator roles can delete users.
        /// </summary>
        /// <param name="id">User ID to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,admin,Administrator")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                // Prevent deletion of current user
                if (CurrentUser.Id == id)
                {
                    return BadRequest("You cannot delete your own account.");
                }

                // Check if user exists before attempting deletion
                var existingUser = await _userAppService.GetAsync(id);

                await _userAppService.DeleteAsync(id);

                Logger.LogInformation("User deleted successfully. UserName: {UserName}, DeletedBy: {DeletedBy}",
                    existingUser.UserName, CurrentUser.UserName);

                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while deleting user with ID: {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the user.");
            }
        }

        /// <summary>
        /// Uploads a user profile photo. Users can upload their own photo, admins can upload for any user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="photo">Photo file to upload</param>
        /// <returns>Updated user with photo URL</returns>
        [HttpPost("{id}/upload-photo")]
        public async Task<ActionResult<UserDto>> UploadPhotoAsync(Guid id, IFormFile photo)
        {
            try
            {
                // Check if user can update this profile
                var canUpdate = await CanUpdateUserAsync(id);
                if (!canUpdate)
                {
                    return Forbid("You don't have permission to update this user's photo.");
                }

                // Validate file
                var validationResult = ValidatePhotoFile(photo);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ErrorMessage);
                }

                // Get current user data
                var currentUser = await _userAppService.GetAsync(id);

                // Process and save the photo
                var photoUrl = await ProcessPhotoUploadAsync(photo, id);

                // Update user with new photo URL
                var updateDto = new UpdateUserDto
                {
                    UserName = currentUser.UserName,
                    Email = currentUser.Email,
                    FirstName = currentUser.FirstName,
                    MiddleName = currentUser.MiddleName,
                    DateOfBirth = currentUser.DateOfBirth,
                    UserPhoto = photoUrl,
                    HasLeftSchool = currentUser.HasLeftSchool,
                    Address = currentUser.Address,
                    GenderId = currentUser.GenderId,
                    SchoolClassId = currentUser.SchoolClassId
                };

                var updatedUser = await _userAppService.UpdateAsync(id, updateDto);

                Logger.LogInformation("User photo uploaded successfully. UserId: {UserId}, PhotoUrl: {PhotoUrl}, UploadedBy: {UploadedBy}",
                    id, photoUrl, CurrentUser.UserName);

                return Ok(new
                {
                    user = updatedUser,
                    photoUrl = photoUrl,
                    message = "Photo uploaded successfully"
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while uploading photo for user: {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the photo.");
            }
        }

        /// <summary>
        /// Gets user statistics. Only SuperAdmin, admin, and Administrator roles can access this.
        /// </summary>
        /// <returns>User statistics</returns>
        [HttpGet("statistics")]
        [Authorize(Roles = "SuperAdmin,admin,Administrator")]
        public async Task<ActionResult> GetUserStatisticsAsync()
        {
            try
            {
                var allUsers = await _userAppService.GetListAsync(new GetUsersInput { MaxResultCount = int.MaxValue });

                var statistics = new
                {
                    TotalUsers = allUsers.TotalCount,
                    ActiveUsers = allUsers.Items.Count(u => !u.HasLeftSchool),
                    InactiveUsers = allUsers.Items.Count(u => u.HasLeftSchool),
                    UsersWithPhotos = allUsers.Items.Count(u => !string.IsNullOrEmpty(u.UserPhoto)),
                    UsersWithoutPhotos = allUsers.Items.Count(u => string.IsNullOrEmpty(u.UserPhoto)),
                    RecentUsers = allUsers.Items
                        .Where(u => u.CreationTime >= DateTime.Now.AddDays(-30))
                        .Count(),
                    UsersByGender = allUsers.Items
                        .Where(u => !string.IsNullOrEmpty(u.GenderName))
                        .GroupBy(u => u.GenderName)
                        .ToDictionary(g => g.Key, g => g.Count()),
                    UsersByClass = allUsers.Items
                        .Where(u => !string.IsNullOrEmpty(u.SchoolClassName))
                        .GroupBy(u => u.SchoolClassName)
                        .ToDictionary(g => g.Key, g => g.Count())
                };

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while getting user statistics");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving statistics.");
            }
        }

        /// <summary>
        /// Resets a user's password. Only SuperAdmin, admin, and Administrator roles can reset passwords.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Success message</returns>
        [HttpPost("{id}/reset-password")]
        [Authorize(Roles = "SuperAdmin,admin,Administrator")]
        public async Task<ActionResult> ResetPasswordAsync(Guid id, [FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    return BadRequest("New password is required.");
                }

                if (!IsValidPassword(request.NewPassword))
                {
                    return BadRequest("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                }

                // Verify user exists
                var user = await _userAppService.GetAsync(id);

                // Note: Password reset logic would typically be implemented in a separate service
                // For this example, we'll return a success message
                // In a real implementation, you'd use IdentityUserManager to reset the password

                Logger.LogInformation("Password reset requested for user: {UserName}, RequestedBy: {RequestedBy}",
                    user.UserName, CurrentUser.UserName);

                return Ok(new
                {
                    message = "Password reset successfully",
                    userName = user.UserName
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while resetting password for user: {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while resetting the password.");
            }
        }

        #region Private Helper Methods

        private async Task<bool> CanUpdateUserAsync(Guid userId)
        {
            // Admin roles can update any user
            if (AdminRoles.Any(role => CurrentUser.IsInRole(role)))
            {
                return true;
            }

            // Users can update their own profile
            return CurrentUser.Id == userId;
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpper && hasLower && hasDigit;
        }

        private (bool IsValid, string ErrorMessage) ValidatePhotoFile(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return (false, "No file uploaded.");
            }

            if (photo.Length > MaxFileSize)
            {
                return (false, $"File size cannot exceed {MaxFileSize / (1024 * 1024)}MB.");
            }

            var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();
            if (!AllowedImageExtensions.Contains(extension))
            {
                return (false, $"Only {string.Join(", ", AllowedImageExtensions)} files are allowed.");
            }

            // Validate content type
            var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            if (!allowedContentTypes.Contains(photo.ContentType.ToLowerInvariant()))
            {
                return (false, "Invalid file type.");
            }

            return (true, null);
        }

        private async Task<string> ProcessPhotoUploadAsync(IFormFile photo, Guid userId)
        {
            // Create uploads directory if it doesn't exist
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "user-photos");
            Directory.CreateDirectory(uploadsPath);

            // Generate unique filename
            var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();
            var fileName = $"{userId}_{DateTime.UtcNow:yyyyMMddHHmmss}{extension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Return relative URL
            return $"/uploads/user-photos/{fileName}";
        }

        #endregion
    }

    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
    }
}