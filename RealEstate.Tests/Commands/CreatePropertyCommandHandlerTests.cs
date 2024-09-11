using Moq;
using NUnit.Framework;
using RealEstate.Application.Commands;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class CreatePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private CreatePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new CreatePropertyCommandHandler(_propertyRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldCreatePropertyAndReturnId()
        {
            // Arrange
            var command = new CreatePropertyCommand
            {
                Name = "Test Property",
                Address = "123 Test St",
                Price = 100000,
                IdOwner = Guid.NewGuid()
            };

            _propertyRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Property>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.Property>()), Times.Once);
            Assert.That(result, Is.TypeOf<Guid>());
        }
    }
}