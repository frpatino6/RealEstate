using MediatR;
using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class UpdatePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private UpdatePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new UpdatePropertyCommandHandler(_propertyRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldUpdateProperty()
        {
            // Arrange
            var command = new UpdatePropertyCommand
            {
                IdProperty = Guid.Parse("b4e5697c-667b-4c90-9115-1ebccfa34ea5"),
                Name = "Updated Property",
                Address = "456 Updated St",
                Price = 120000,
                Year = 2022
            };

            var property = new Property
            {
                IdProperty = command.IdProperty,
                Name = "Original Property",
                Address = "123 Original St",
                Price = 100000,
                Year = 2020
            };

            // Simula que el repositorio devuelve una propiedad existente
            _propertyRepositoryMock.Setup(repo => repo.GetByIdAsync(command.IdProperty))
                .ReturnsAsync(property);

            // Simula la actualización
            _propertyRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Property>()), Times.Once);
            Assert.That(result, Is.EqualTo(Unit.Value));
        }

        [Test]
        public void Handle_PropertyNotFound_ShouldThrowException()
        {
            // Arrange
            var command = new UpdatePropertyCommand
            {
                IdProperty = Guid.NewGuid(),
                Name = "Updated Property",
                Address = "456 Updated St",
                Price = 120000,
                Year = 2022
            };

            // Simula que la propiedad no es encontrada
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