using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Application.Validators;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class AddPropertyImageValidatorTests
    {
        private AddPropertyImageValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AddPropertyImageValidator();
        }

        [Test]
        public void Validate_ValidCommand_ShouldPassValidation()
        {
            var command = new AddPropertyImageCommand
            {
                IdProperty = Guid.NewGuid(),
                File = "image.jpg",
                Enabled = true,
                Location = "images/property"

            };

            var result = _validator.Validate(command);

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Validate_InvalidCommand_ShouldFailValidation()
        {
            var command = new AddPropertyImageCommand
            {
                IdProperty = Guid.Empty,
                File = "",
                Enabled = true
            };

            var result = _validator.Validate(command);

            Assert.That(result.IsValid, Is.False);
            Assert.That(3, Is.EqualTo(result.Errors.Count));
        }
    }
}