using Moq;
using NUnit.Framework;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetOwnerByIdQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private GetOwnerByIdQueryHandler _handler;
        private Mock<IMapperOwnerService> _mapperOwnerService;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new GetOwnerByIdQueryHandler(_ownerRepositoryMock.Object, _mapperOwnerService.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOwner_WhenOwnerExists()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var owner = new Owner
            {
                IdOwner = ownerId,
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "johndoe.jpg",
                Birthday = new DateTime(1985, 5, 1)
            };

            // Simula que el repositorio devuelve un propietario existente
            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(ownerId))
                .ReturnsAsync(owner);

            // Act
            var result = await _handler.Handle(new GetOwnerByIdQuery { IdOwner = ownerId }, CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(ownerId), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Address, Is.EqualTo("123 Main St"));
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenOwnerDoesNotExist()
        {
            // Arrange
            var ownerId = Guid.NewGuid();

            // Simula que el repositorio devuelve `null` cuando no encuentra al propietario
            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(ownerId))
                .ReturnsAsync((Owner)null);

            // Act
            var result = await _handler.Handle(new GetOwnerByIdQuery { IdOwner = ownerId }, CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(ownerId), Times.Once);
            Assert.That(result, Is.Null);
        }
    }
}