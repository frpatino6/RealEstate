using Moq;
using NUnit.Framework;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Commands
{
    [TestFixture]
    public class PropertyRepositoryTests
    {
        private Mock<IPropertyRepository> _propertyRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnProperties()
        {
            var properties = new List<Property>
        {
            new Property { Name = "Property 1" },
            new Property { Name = "Property 2" }
        };

            _propertyRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(properties);

            var result = await _propertyRepositoryMock.Object.GetAllAsync();

            Assert.That(2, Is.EqualTo(result.Count()));
        }
    }
}