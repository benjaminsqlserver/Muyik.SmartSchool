using System;
using Shouldly;
using Xunit;
using Muyik.SmartSchool.Users;

namespace Muyik.SmartSchool.Domain.Tests.Users
{
    /// <summary>
    /// Contains unit tests for the <see cref="AppUser"/> domain entity class.
    /// 
    /// These tests validate the behavior of the <see cref="AppUser"/> constructor,
    /// ensuring proper handling of both required and optional parameters, as well as input validation.
    /// 
    /// The test methods follow the Arrange-Act-Assert pattern and use:
    /// - <c>Xunit</c> for test case declaration and execution.
    /// - <c>Shouldly</c> for expressive assertions.
    /// </summary>
    public class AppUserTests : SmartSchoolDomainTestBase<SmartSchoolDomainModule>
    {
        /// <summary>
        /// Verifies that an AppUser is created correctly when all valid parameters are supplied.
        /// This includes setting all properties and ensuring no exception is thrown.
        /// </summary>
        [Fact]
        public void Constructor_ValidParameters_ShouldCreateAppUser()
        {
            // Arrange: Define valid values for all AppUser constructor parameters
            var id = Guid.NewGuid();
            var userName = "testuser";
            var email = "test@example.com";
            var firstName = "Test";
            var middleName = "Middle";
            var dateOfBirth = DateTime.Now.AddYears(-20); // Simulate 20 years old
            var userPhoto = "photo.jpg";
            var hasLeftSchool = false;
            var address = "123 Test St";
            var genderId = Guid.NewGuid();
            var schoolClassId = Guid.NewGuid();

            // Act: Create an instance of AppUser with the provided values
            var user = new AppUser(id, userName, email, firstName, middleName,
                dateOfBirth, userPhoto, hasLeftSchool, address, genderId, schoolClassId);

            // Assert: Verify that all fields were correctly assigned
            user.Id.ShouldBe(id);
            user.UserName.ShouldBe(userName);
            user.Email.ShouldBe(email);
            user.FirstName.ShouldBe(firstName);
            user.MiddleName.ShouldBe(middleName);
            user.DateOfBirth.ShouldBe(dateOfBirth);
            user.UserPhoto.ShouldBe(userPhoto);
            user.HasLeftSchool.ShouldBe(hasLeftSchool);
            user.Address.ShouldBe(address);
            user.GenderId.ShouldBe(genderId);
            user.SchoolClassId.ShouldBe(schoolClassId);
        }

        /// <summary>
        /// Verifies that the AppUser constructor allows null values for optional parameters
        /// such as FirstName, MiddleName, DateOfBirth, UserPhoto, Address, GenderId, and SchoolClassId.
        /// </summary>
        [Fact]
        public void Constructor_NullOptionalParameters_ShouldCreateAppUser()
        {
            // Arrange: Only required parameters are set, all optional ones are null
            var id = Guid.NewGuid();
            var userName = "testuser";
            var email = "test@example.com";

            // Act: Instantiate AppUser with nulls for optional fields
            var user = new AppUser(id, userName, email, null, null, null, null, false, null, null, null);

            // Assert: Ensure required fields are set correctly and optional fields are null
            user.Id.ShouldBe(id);
            user.UserName.ShouldBe(userName);
            user.Email.ShouldBe(email);
            user.FirstName.ShouldBeNull();
            user.MiddleName.ShouldBeNull();
            user.DateOfBirth.ShouldBeNull();
            user.UserPhoto.ShouldBeNull();
            user.HasLeftSchool.ShouldBe(false);
            user.Address.ShouldBeNull();
            user.GenderId.ShouldBeNull();
            user.SchoolClassId.ShouldBeNull();
        }

        /// <summary>
        /// Ensures that the AppUser constructor throws an ArgumentException
        /// when an invalid UserName is passed (null, empty, or whitespace).
        /// </summary>
        /// <param name="userName">Invalid username value to test</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_InvalidUserName_ShouldThrowException(string userName)
        {
            // Arrange: Set a valid email, and invalid username from test data
            var id = Guid.NewGuid();
            var email = "test@example.com";

            // Act & Assert: Constructor should throw ArgumentException for invalid usernames
            Should.Throw<ArgumentException>(() =>
                new AppUser(id, userName, email, null, null, null, null, false, null, null, null));
        }

        /// <summary>
        /// Ensures that the AppUser constructor throws an ArgumentException
        /// when an invalid Email is passed (null, empty, whitespace, or malformed).
        /// </summary>
        /// <param name="email">Invalid email value to test</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("invalid-email")]
        public void Constructor_InvalidEmail_ShouldThrowException(string email)
        {
            // Arrange: Set a valid username, and invalid email from test data
            var id = Guid.NewGuid();
            var userName = "testuser";

            // Act & Assert: Constructor should throw ArgumentException for invalid email values
            Should.Throw<ArgumentException>(() =>
                new AppUser(id, userName, email, null, null, null, null, false, null, null, null));
        }
    }
}

