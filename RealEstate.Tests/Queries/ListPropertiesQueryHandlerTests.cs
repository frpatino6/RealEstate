using Moq;
using NUnit.Framework;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class ListPropertiesQueryHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IPropertyFilter> _propertyFilterMock;
        private ListPropertiesQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _propertyFilterMock = new Mock<IPropertyFilter>();
            _handler = new ListPropertiesQueryHandler(_propertyRepositoryMock.Object, _propertyFilterMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFilteredProperties()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 1" },
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 2" }
        };

            var filteredProperties = properties.Take(1);

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            _propertyFilterMock.Setup(filter => filter.Apply(properties, It.IsAny<ListPropertiesQuery>()))
                .Returns(filteredProperties);

            // Act
            var result = await _handler.Handle(new ListPropertiesQuery(), CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _propertyFilterMock.Verify(filter => filter.Apply(properties, It.IsAny<ListPropertiesQuery>()), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property 1"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllProperties_WhenNoFilterIsApplied()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 1" },
            new Property { IdProperty = Guid.NewGuid(), Name = "Property 2" }
        };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            _propertyFilterMock.Setup(filter => filter.Apply(properties, It.IsAny<ListPropertiesQuery>()))
                .Returns(properties);

            // Act
            var result = await _handler.Handle(new ListPropertiesQuery(), CancellationToken.None);

            // Assert
            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _propertyFilterMock.Verify(filter => filter.Apply(properties, It.IsAny<ListPropertiesQuery>()), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}