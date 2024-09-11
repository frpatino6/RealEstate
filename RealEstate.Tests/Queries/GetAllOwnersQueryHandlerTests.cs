using Moq;
using NUnit.Framework;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetAllOwnersQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private GetAllOwnersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new GetAllOwnersQueryHandler(_ownerRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnAllOwners()
        {
            // Arrange
            var owners = new List<Owner>
        {
            new Owner { IdOwner = Guid.NewGuid(), Name = "John Doe", Address = "123 Main St" },
            new Owner { IdOwner = Guid.NewGuid(), Name = "Jane Doe", Address = "456 Another St" }
        };

            // Simula que el repositorio devuelve una lista de propietarios
            _ownerRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(owners);

            // Act
            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.That(2, Is.EqualTo(result.Count()));
            Assert.That("John Doe", Is.EqualTo(result.First().Name));
            Assert.That("Jane Doe", Is.EqualTo(result.Last().Name));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoOwnersExist()
        {
            // Arrange
            var owners = new List<Owner>();  // Lista vacía de propietarios

            // Simula que el repositorio devuelve una lista vacía
            _ownerRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(owners);

            // Act
            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            // Assert
            _ownerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Empty);
        }
    }
}