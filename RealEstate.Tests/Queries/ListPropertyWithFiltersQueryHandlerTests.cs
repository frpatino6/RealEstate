using Moq;
using NUnit.Framework;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class ListPropertyWithFiltersQueryHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private ListPropertyWithFiltersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new ListPropertyWithFiltersQueryHandler(_propertyRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldFilterPropertiesByMinPrice()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
        };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            var query = new ListPropertyWithFiltersQuery
            {
                MinPrice = 180000
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property 2"));
        }

        [Test]
        public async Task Handle_ShouldFilterPropertiesByMaxPrice()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
        };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            var query = new ListPropertyWithFiltersQuery
            {
                MaxPrice = 160000
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property 1"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllProperties_WhenNoFiltersAreApplied()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
        };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            var query = new ListPropertyWithFiltersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}