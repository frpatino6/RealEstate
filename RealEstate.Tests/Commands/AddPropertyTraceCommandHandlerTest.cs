namespace RealEstate.Tests.Commands
{
    using Moq;
    using NUnit.Framework;
    using RealEstate.Application.Commands;
    using RealEstate.Domain.Entities;
    using RealEstate.Domain.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class AddPropertyTraceCommandHandlerTests
    {
        private Mock<IPropertyTraceRepository> _propertyTraceRepositoryMock;
        private AddPropertyTraceCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();
            _handler = new AddPropertyTraceCommandHandler(_propertyTraceRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldAddPropertyTraceAndReturnId()
        {
            // Arrange
            var command = new AddPropertyTraceCommand
            {
                IdProperty = Guid.NewGuid(),
                DateSale = DateTime.UtcNow,
                Name = "Test Trace",
                Value = 100000,
                Tax = 10000
            };

            // Simula la adición de un rastro de propiedad
            _propertyTraceRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<PropertyTrace>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _propertyTraceRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PropertyTrace>()), Times.Once);
            Assert.That(result, Is.TypeOf<Guid>());
        }

        [Test]
        public async Task Handle_ValidCommand_ShouldReturnCorrectIdForPropertyTrace()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var command = new AddPropertyTraceCommand
            {
                IdProperty = Guid.NewGuid(),
                DateSale = DateTime.UtcNow,
                Name = "Test Trace",
                Value = 100000,
                Tax = 10000
            };

            // Simula el retorno del ID del rastro recién creado
            _propertyTraceRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<PropertyTrace>()))
                .Callback<PropertyTrace>(trace => trace.IdPropertyTrace = expectedId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(expectedId, Is.EqualTo(result));
        }
    }

}
