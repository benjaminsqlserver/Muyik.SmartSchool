using System;
using Shouldly;
using Xunit;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Domain.Tests.Entities
{
    /// <summary>
    /// Unit tests for the <see cref="Gender"/> entity in the domain layer.
    /// </summary>
    public class GenderTests : SmartSchoolDomainTestBase<SmartSchoolDomainModule>
    {
        /// <summary>
        /// Verifies that a <see cref="Gender"/> object can be created and initialized
        /// properly with valid values for its properties.
        /// </summary>
        [Fact]
        public void Constructor_ValidParameters_ShouldCreateGender()
        {
            // Arrange
            var id = Guid.NewGuid();
            var genderName = "Male";
            var description = "Male gender";

            // Act: Use the public constructor
            var gender = new Gender(id, genderName, description);

            // Assert
            gender.Id.ShouldBe(id);
            gender.GenderName.ShouldBe(genderName);
            gender.Description.ShouldBe(description);
        }

        /// <summary>
        /// Tests constructor with minimal parameters
        /// </summary>
        [Fact]
        public void Constructor_WithoutDescription_ShouldCreateGender()
        {
            // Arrange
            var id = Guid.NewGuid();
            var genderName = "Female";

            // Act: Use the public constructor without description
            var gender = new Gender(id, genderName);

            // Assert
            gender.Id.ShouldBe(id);
            gender.GenderName.ShouldBe(genderName);
            gender.Description.ShouldBeNull();
        }

        /// <summary>
        /// Tests validation scenarios - you would typically add domain validation
        /// to your Gender entity constructor to throw exceptions for invalid data
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_InvalidGenderName_ShouldThrowException(string invalidGenderName)
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert: This would work if you add validation to your constructor
            // For now, this test documents expected behavior
            Should.Throw<ArgumentException>(() => new Gender(id, invalidGenderName));
        }
    }
}