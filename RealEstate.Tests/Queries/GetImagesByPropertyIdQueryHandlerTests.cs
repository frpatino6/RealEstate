using Moq;
using NUnit.Framework;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetImagesByPropertyIdQueryHandlerTests
    {
        private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
        private GetImagesByPropertyIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            _handler = new GetImagesByPropertyIdQueryHandler(_propertyImageRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnImagesForProperty()
        {
            var propertyId = Guid.NewGuid();
            var images = new List<PropertyImage>
        {
            new PropertyImage { IdPropertyImage = Guid.NewGuid(), IdProperty = propertyId, File = "image1.jpg", Enabled = true },
            new PropertyImage { IdPropertyImage = Guid.NewGuid(), IdProperty = propertyId, File = "image2.jpg", Enabled = true }
        };

            _propertyImageRepositoryMock.Setup(repo => repo.GetByPropertyIdAsync(propertyId))
                .ReturnsAsync(images);

            var result = await _handler.Handle(new GetImagesByPropertyIdQuery { IdProperty = propertyId }, CancellationToken.None);

            _propertyImageRepositoryMock.Verify(repo => repo.GetByPropertyIdAsync(propertyId), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().File, Is.EqualTo("image1.jpg"));
            Assert.That(result.Last().File, Is.EqualTo("image2.jpg"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoImagesExistForProperty()
        {
            var propertyId = Guid.NewGuid();
            var images = new List<PropertyImage>();      

            _propertyImageRepositoryMock.Setup(repo => repo.GetByPropertyIdAsync(propertyId))
                .ReturnsAsync(images);

            var result = await _handler.Handle(new GetImagesByPropertyIdQuery { IdProperty = propertyId }, CancellationToken.None);

            _propertyImageRepositoryMock.Verify(repo => repo.GetByPropertyIdAsync(propertyId), Times.Once);
            Assert.That(result, Is.Empty);
        }
    }
}