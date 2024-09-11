using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class CreateOwnerCommandHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private CreateOwnerCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new CreateOwnerCommandHandler(_ownerRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldCreateOwnerAndReturnId()
        {
            // Arrange
            var command = new CreateOwnerCommand
            {
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "johndoe.jpg",
                Birthday = new DateTime(1985, 5, 1)
            };

            // Simula la adición de un nuevo propietario
            _ownerRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Owner>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Owner>()), Times.Once);
            Assert.That(result, Is.TypeOf<Guid>());
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnCorrectIdForOwner()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var command = new CreateOwnerCommand
            {
                Name = "Jane Doe",
                Address = "456 Another St",
                Photo = "janedoe.jpg",
                Birthday = new DateTime(1990, 7, 15)
            };

            // Simula el retorno del ID del nuevo propietario
            _ownerRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Owner>()))
                .Callback<Owner>(owner => owner.IdOwner = expectedId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(expectedId, Is.EqualTo(result));
        }
    }
}