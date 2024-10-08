﻿using Moq;
using NUnit.Framework;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Tests.Queries
{
    [TestFixture]
    public class GetTracesByPropertyIdQueryHandlerTests
    {
        private Mock<IPropertyTraceRepository> _propertyTraceRepositoryMock;
        private GetTracesByPropertyIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();
            _handler = new GetTracesByPropertyIdQueryHandler(_propertyTraceRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnTracesForProperty_WhenTracesExist()
        {
            var propertyId = Guid.NewGuid();
            var traces = new List<PropertyTrace>
        {
            new PropertyTrace { IdPropertyTrace = Guid.NewGuid(), IdProperty = propertyId, Name = "Trace 1", Value = 100000, Tax = 5000 },
            new PropertyTrace { IdPropertyTrace = Guid.NewGuid(), IdProperty = propertyId, Name = "Trace 2", Value = 200000, Tax = 10000 }
        };

            _propertyTraceRepositoryMock.Setup(repo => repo.GetByPropertyIdAsync(propertyId))
                .ReturnsAsync(traces);

            var result = await _handler.Handle(new GetTracesByPropertyIdQuery { IdProperty = propertyId }, CancellationToken.None);

            _propertyTraceRepositoryMock.Verify(repo => repo.GetByPropertyIdAsync(propertyId), Times.Once);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Trace 1"));
            Assert.That(result.Last().Name, Is.EqualTo("Trace 2"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoTracesExistForProperty()
        {
            var propertyId = Guid.NewGuid();
            var traces = new List<PropertyTrace>();        

            _propertyTraceRepositoryMock.Setup(repo => repo.GetByPropertyIdAsync(propertyId))
                .ReturnsAsync(traces);

            var result = await _handler.Handle(new GetTracesByPropertyIdQuery { IdProperty = propertyId }, CancellationToken.None);

            _propertyTraceRepositoryMock.Verify(repo => repo.GetByPropertyIdAsync(propertyId), Times.Once);
            Assert.That(result, Is.Empty);
        }
    }
}