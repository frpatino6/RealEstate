using Moq;
using NUnit.Framework;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetAllOwnersQueryHandlerTests
    {
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private Mock<IMapperOwnerService> _mapperOwnerServiceMock;
        private GetAllOwnersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mapperOwnerServiceMock = new Mock<IMapperOwnerService>();
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _handler = new GetAllOwnersQueryHandler(_ownerRepositoryMock.Object, _mapperOwnerServiceMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnAllOwners()
        {
            var owners = new List<Owner>
            {
                new Owner { IdOwner = Guid.NewGuid(), Name = "John Doe", Address = "123 Main St" },
                new Owner { IdOwner = Guid.NewGuid(), Name = "Jane Doe", Address = "456 Another St" }
            };

            var ownerDtos = new List<OwnerDto>
            {
                new OwnerDto(Guid.NewGuid(), "John Doe", "123 Main St", "john.jpg", new DateTime(1980, 1, 1)),
                new OwnerDto(Guid.NewGuid(), "Jane Doe", "456 Another St", "jane.jpg", new DateTime(1982, 1, 1))
            };

            _ownerRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(owners);

            _mapperOwnerServiceMock.Setup(mapper => mapper.MapToOwners(owners))
                .Returns(ownerDtos);

            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            _ownerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperOwnerServiceMock.Verify(mapper => mapper.MapToOwners(owners), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("John Doe"));
            Assert.That(result.Last().Name, Is.EqualTo("Jane Doe"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoOwnersExist()
        {
            var owners = new List<Owner>();      
            var ownerDtos = new List<OwnerDto>();     

            _ownerRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(owners);

            _mapperOwnerServiceMock.Setup(mapper => mapper.MapToOwners(owners))
                .Returns(ownerDtos);

            var result = await _handler.Handle(new GetAllOwnersQuery(), CancellationToken.None);

            _ownerRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperOwnerServiceMock.Verify(mapper => mapper.MapToOwners(owners), Times.Once);
            Assert.That(result, Is.Empty);
        }
    }
}
