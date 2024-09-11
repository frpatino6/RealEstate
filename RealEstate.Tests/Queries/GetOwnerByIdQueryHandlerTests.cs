using Moq;
using NUnit.Framework;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetOwnerByIdQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private Mock<IMapperOwnerService> _mapperOwnerServiceMock;
        private GetOwnerByIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _mapperOwnerServiceMock = new Mock<IMapperOwnerService>();

            _handler = new GetOwnerByIdQueryHandler(_ownerRepositoryMock.Object, _mapperOwnerServiceMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnOwner_WhenOwnerExists()
        {
            var ownerId = Guid.NewGuid();
            var owner = new Owner
            {
                IdOwner = ownerId,
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "johndoe.jpg",
                Birthday = new DateTime(1985, 5, 1)
            };
            var ownerDto = new OwnerDto(
                ownerId,                
                "John Doe",             
                "123 Main St",          
                "johndoe.jpg",          
                new DateTime(1985, 5, 1) 
            );


            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(ownerId))
                .ReturnsAsync(owner);

            _mapperOwnerServiceMock.Setup(mapper => mapper.MapToOwner(owner))
                .Returns(ownerDto);

            var result = await _handler.Handle(new GetOwnerByIdQuery { IdOwner = ownerId }, CancellationToken.None);

            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(ownerId), Times.Once);
            _mapperOwnerServiceMock.Verify(mapper => mapper.MapToOwner(owner), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Address, Is.EqualTo("123 Main St"));
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenOwnerDoesNotExist()
        {
            var ownerId = Guid.NewGuid();

            _ownerRepositoryMock.Setup(repo => repo.GetByIdAsync(ownerId))
                .ReturnsAsync((Owner)null);

            var result = await _handler.Handle(new GetOwnerByIdQuery { IdOwner = ownerId }, CancellationToken.None);

            _ownerRepositoryMock.Verify(repo => repo.GetByIdAsync(ownerId), Times.Once);
            _mapperOwnerServiceMock.Verify(mapper => mapper.MapToOwner(It.IsAny<Owner>()), Times.Never); 
            Assert.That(result, Is.Null);
        }
    }
}
