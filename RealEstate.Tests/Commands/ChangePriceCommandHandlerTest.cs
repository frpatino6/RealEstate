using MediatR;
using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class ChangePriceCommandHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private ChangePriceCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new ChangePriceCommandHandler(_propertyRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldChangePriceAndUpdateProperty()
        {
            // Arrange
            var propertyId = Guid.NewGuid();
            var property = new Property
            {
                IdProperty = propertyId,
                Price = 100000
            };

            var command = new ChangePriceCommand
            {
                IdProperty = propertyId,
                NewPrice = 150000
            };

            // Simula que la propiedad existe en el repositorio
            _propertyRepositoryMock.Setup(repo => repo.GetByIdAsync(propertyId))
                .ReturnsAsync(property);

            // Simula la actualización de la propiedad
            _propertyRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetByIdAsync(propertyId), Times.Once);
            _propertyRepositoryMock.Verify(repo => repo.UpdateAsync(property), Times.Once);
            Assert.That(150000, Is.EqualTo(property.Price));
            Assert.That(Unit.Value, Is.EqualTo(result));
        }

        [Test]
        public void Handle_PropertyNotFound_ShouldThrowException()
        {
            // Arrange
            var command = new ChangePriceCommand
            {
                IdProperty = Guid.NewGuid(),
                NewPrice = 150000
            };

            // Simula que la propiedad no existe en el repositorio
            _propertyRepositoryMock.Setup(repo => repo.GetByIdAsync(command.IdProperty))
                .ReturnsAsync((Property)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That("Property not found", Is.EqualTo(ex.Message));
            _propertyRepositoryMock.Verify(repo => repo.GetByIdAsync(command.IdProperty), Times.Once);
            _propertyRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Property>()), Times.Never);
        }
    }
}