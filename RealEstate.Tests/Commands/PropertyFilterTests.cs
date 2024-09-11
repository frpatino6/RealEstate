using NUnit.Framework;
using RealEstate.Application.Queries;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class PropertyFilterTests
    {
        private PropertyFilter _filter;

        [SetUp]
        public void Setup()
        {
            _filter = new PropertyFilter();
        }

        [Test]
        public void Apply_FilterByAddress_ShouldReturnFilteredProperties()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { Address = "123 Test St", Price = 100000 },
            new Property { Address = "456 Another St", Price = 200000 }
        };

            var query = new ListPropertiesQuery
            {
                Address = "Test"
            };

            // Act
            var result = _filter.Apply(properties, query);

            // Assert
            Assert.That(1, Is.EqualTo(result.Count()));
            Assert.That("123 Test St", Is.EqualTo(result.First().Address));
        }

        [Test]
        public void Apply_FilterByPriceRange_ShouldReturnFilteredProperties()
        {
            // Arrange
            var properties = new List<Property>
        {
            new Property { Address = "123 Test St", Price = 100000 },
            new Property { Address = "456 Another St", Price = 200000 }
        };

            var query = new ListPropertiesQuery
            {
                MinPrice = 150000
            };

            // Act
            var result = _filter.Apply(properties, query);

            // Assert
            Assert.That(1, Is.EqualTo(result.Count()));
            Assert.That(200000, Is.EqualTo(result.First().Price));
        }
    }
}