using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Application.Validators;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class CreatePropertyValidatorTests
    {
        private CreatePropertyValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreatePropertyValidator();
        }

        [Test]
        public void Validate_ValidCommand_ShouldPassValidation()
        {
            // Arrange
            var command = new CreatePropertyCommand
            {
                CodeInternal = "Code",
                Name = "Test Property",
                Address = "123 Test St",
                Price = 100000,
                Location = "Test City",
                IdOwner = Guid.NewGuid()
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Validate_InvalidCommand_ShouldFailValidation()
        {
            // Arrange
            var command = new CreatePropertyCommand
            {
                Name = "",
                Address = "",
                Price = -1,  // Invalid price
                IdOwner = Guid.Empty,
                CodeInternal = string.Empty,
                Location = string.Empty,
                Year = 2000
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(6, Is.EqualTo(result.Errors.Count));
        }
    }
}