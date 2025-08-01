using System;
using Shouldly;
using Xunit;
using Muyik.SmartSchool.Entities;

namespace Muyik.SmartSchool.Domain.Tests.Entities
{
    /// <summary>
    /// Contains unit tests for the <see cref="SchoolClass"/> entity in the domain layer.
    /// 
    /// These tests verify the correct creation of a SchoolClass object,
    /// ensuring its properties can be initialized and accessed correctly.
    /// 
    /// This test class focuses on constructor behavior and validation scenarios.
    /// </summary>
    public class SchoolClassTests : SmartSchoolDomainTestBase<SmartSchoolDomainModule>
    {
        /// <summary>
        /// Verifies that a <see cref="SchoolClass"/> object can be successfully created
        /// when valid values are provided for the constructor parameters.
        /// </summary>
        [Fact]
        public void Constructor_ValidParameters_ShouldCreateSchoolClass()
        {
            // Arrange: Define valid test data
            var id = Guid.NewGuid();
            var className = "Grade 1A";
            var description = "First grade, section A";

            // Act: Use the public constructor
            var schoolClass = new SchoolClass(id, className, description);

            // Assert: Verify that the object's properties were assigned correctly
            schoolClass.Id.ShouldBe(id);
            schoolClass.ClassName.ShouldBe(className);
            schoolClass.Description.ShouldBe(description);
        }

        /// <summary>
        /// Verifies that a <see cref="SchoolClass"/> object can be created
        /// without providing a description (optional parameter).
        /// </summary>
        [Fact]
        public void Constructor_WithoutDescription_ShouldCreateSchoolClass()
        {
            // Arrange
            var id = Guid.NewGuid();
            var className = "Grade 2B";

            // Act: Use constructor without description
            var schoolClass = new SchoolClass(id, className);

            // Assert
            schoolClass.Id.ShouldBe(id);
            schoolClass.ClassName.ShouldBe(className);
            schoolClass.Description.ShouldBeNull();
        }

        /// <summary>
        /// Tests various valid class name scenarios.
        /// </summary>
        [Theory]
        [InlineData("Grade 1A")]
        [InlineData("Kindergarten")]
        [InlineData("Pre-K Blue")]
        [InlineData("Advanced Mathematics")]
        public void Constructor_ValidClassNames_ShouldCreateSchoolClass(string className)
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var schoolClass = new SchoolClass(id, className);

            // Assert
            schoolClass.ClassName.ShouldBe(className);
            schoolClass.Id.ShouldBe(id);
        }

        /// <summary>
        /// Tests that invalid class names should be handled appropriately.
        /// Note: Add validation to your SchoolClass constructor to make this test meaningful.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidClassName_ShouldThrowException(string invalidClassName)
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert
            // This test assumes you'll add validation to your constructor
            Should.Throw<ArgumentException>(() => new SchoolClass(id, invalidClassName));
        }

        /// <summary>
        /// Tests that class names exceeding the maximum length should be handled.
        /// </summary>
        [Fact]
        public void Constructor_ClassNameTooLong_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var longClassName = new string('A', 101); // Exceeds 100 character limit

            // Act & Assert
            // This test assumes you'll add validation to your constructor
            Should.Throw<ArgumentException>(() => new SchoolClass(id, longClassName));
        }

        /// <summary>
        /// Tests that descriptions exceeding the maximum length should be handled.
        /// </summary>
        [Fact]
        public void Constructor_DescriptionTooLong_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var className = "Valid Class";
            var longDescription = new string('A', 201); // Exceeds 200 character limit

            // Act & Assert
            // This test assumes you'll add validation to your constructor
            Should.Throw<ArgumentException>(() => new SchoolClass(id, className, longDescription));
        }
    }
}