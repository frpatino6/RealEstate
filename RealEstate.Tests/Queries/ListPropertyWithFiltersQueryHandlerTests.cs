using Moq;
using NUnit.Framework;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class ListPropertyWithFiltersQueryHandlerTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IMapperPropertyService> _mapperPropertyServiceMock;
        private Mock<IPropertyFilter> _propertyFilterMock;

        private ListPropertyWithFiltersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mapperPropertyServiceMock = new Mock<IMapperPropertyService>();
            _propertyFilterMock = new Mock<IPropertyFilter>();

            _handler = new ListPropertyWithFiltersQueryHandler(
                _propertyRepositoryMock.Object,
                _propertyFilterMock.Object,
                _mapperPropertyServiceMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldFilterPropertiesByMinPrice_AndMapToDto()
        {
            var properties = new List<Property>
            {
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
            };

            var filteredProperties = properties.Where(p => p.Price >= 180000).ToList();

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            _propertyFilterMock.Setup(filter => filter.Apply(properties, It.IsAny<ListPropertyWithFiltersQuery>()))
                .Returns(filteredProperties);

            _mapperPropertyServiceMock.Setup(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()))
                 .Returns((IEnumerable<Property> properties) => properties.Select(p => new PropertyDto(
                     IdProperty: p.IdProperty,
                     Name: p.Name,
                     Address: p.Address,
                     Price: p.Price,
                     Year: p.Year
                 )));


            var query = new ListPropertyWithFiltersQuery
            {
                MinPrice = 180000
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _propertyFilterMock.Verify(filter => filter.Apply(properties, query), Times.Once);
            _mapperPropertyServiceMock.Verify(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()), Times.Exactly(filteredProperties.Count));

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property 2"));
        }

        [Test]
        public async Task Handle_ShouldFilterPropertiesByMaxPrice_AndMapToDto()
        {
            var properties = new List<Property>
            {
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
            };

            var filteredProperties = properties.Where(p => p.Price <= 160000).ToList();

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            _propertyFilterMock.Setup(filter => filter.Apply(properties, It.IsAny<ListPropertyWithFiltersQuery>()))
                .Returns(filteredProperties);

            _mapperPropertyServiceMock.Setup(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()))
                .Returns((IEnumerable<Property> properties) => properties.Select(p => new PropertyDto(IdProperty: p.IdProperty, Name: p.Name, Address: p.Address, Price: p.Price, Year: p.Year)));

            var query = new ListPropertyWithFiltersQuery
            {
                MaxPrice = 160000
            };

            var result = await _handler.Handle(query, CancellationToken.None);

            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _propertyFilterMock.Verify(filter => filter.Apply(properties, query), Times.Once);
            _mapperPropertyServiceMock.Verify(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()), Times.Exactly(filteredProperties.Count));

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Property 1"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllProperties_WhenNoFiltersAreApplied_AndMapToDto()
        {
            var properties = new List<Property>
            {
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 1", Price = 150000 },
                new Property { IdProperty = Guid.NewGuid(), Name = "Property 2", Price = 200000 }
            };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            _propertyFilterMock.Setup(filter => filter.Apply(properties, It.IsAny<ListPropertyWithFiltersQuery>()))
                .Returns(properties);

            _mapperPropertyServiceMock.Setup(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()))
                .Returns((IEnumerable<Property> properties) => properties.Select(p => new PropertyDto(IdProperty: p.IdProperty, Name: p.Name, Address: p.Address, Price: p.Price, Year: p.Year)));

            var query = new ListPropertyWithFiltersQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            _propertyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _propertyFilterMock.Verify(filter => filter.Apply(properties, query), Times.Once);
            _mapperPropertyServiceMock.Verify(mapper => mapper.MapToDto(It.IsAny<IEnumerable<Property>>()), Times.Exactly(1));

            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
