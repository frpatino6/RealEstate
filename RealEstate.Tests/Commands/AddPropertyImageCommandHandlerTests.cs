using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class AddPropertyImageCommandHandlerTests
    {
        private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
        private AddPropertyImageCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            _handler = new AddPropertyImageCommandHandler(_propertyImageRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldAddPropertyImageAndReturnId()
        {
            // Arrange
            var command = new AddPropertyImageCommand
            {
                IdProperty = Guid.NewGuid(),
                File = "testimage.jpg",
                Enabled = true
            };

            // Simula la adición de una imagen de propiedad
            _propertyImageRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _propertyImageRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
            Assert.That(result, Is.TypeOf<Guid>());
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnCorrectIdForPropertyImage()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var command = new AddPropertyImageCommand
            {
                IdProperty = Guid.NewGuid(),
                File = "testimage.jpg",
                Enabled = true
            };

            // Simula el retorno del ID de la imagen recién creada
            _propertyImageRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<PropertyImage>()))
                .Callback<PropertyImage>(image => image.IdPropertyImage = expectedId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(expectedId, Is.EqualTo(result));
        }
    }
}