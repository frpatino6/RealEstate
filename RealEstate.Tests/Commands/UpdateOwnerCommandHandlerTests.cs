using MediatR;
using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class UpdateOwnerCommandHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private UpdateOwnerCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new UpdateOwnerCommandHandler(_ownerRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldUpdateOwner()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var owner = new Owner
            {
                IdOwner = ownerId,
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "oldphoto.jpg",
                Birthday = new DateTime(1985, 5, 1)
            };

            var command = new UpdateOwnerCommand
            {
                IdOwner = ownerId,
                Name = "Jane Doe",
                Address = "456 Updated St",
                Photo = "newphoto.jpg",
                Birthday = new DateTime(1990, 7, 15)
            };

            // Simula que el propietario existe en el repositorio
            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(ownerId))
                .ReturnsAsync(owner);

            // Simula la actualización del propietario
            _ownerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Owner>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(ownerId), Times.Once);
            _ownerRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Owner>()), Times.Once);
            Assert.That("Jane Doe", Is.EqualTo(owner.Name));
            Assert.That("456 Updated St", Is.EqualTo(owner.Address));
            Assert.That("newphoto.jpg", Is.EqualTo(owner.Photo));
            Assert.That(new DateTime(1990, 7, 15), Is.EqualTo(owner.Birthday));
            Assert.That(Unit.Value, Is.EqualTo(result));
        }

        [Test]
        public void Handle_OwnerNotFound_ShouldThrowException()
        {
            // Arrange
            var command = new UpdateOwnerCommand
            {
                IdOwner = Guid.NewGuid(),
                Name = "Jane Doe",
                Address = "456 Updated St",
                Photo = "newphoto.jpg",
                Birthday = new DateTime(1990, 7, 15)
            };

            // Simula que el propietario no existe en el repositorio
            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(command.IdOwner))
                .ReturnsAsync((Owner)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That("Owner not found", Is.EqualTo(ex.Message));
            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(command.IdOwner), Times.Once);
            _ownerRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Owner>()), Times.Never);
        }
    }
}