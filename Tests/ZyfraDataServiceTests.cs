using Moq;
using System.Collections.Generic;
using Xunit;
using ZyfraServer.Models;
using ZyfraServer.Interfaces.Repositories;
using ZyfraServer.Servieces;

namespace ZyfraServer.Tests
{
    public class ZyfraDataServiceTests
    {
        private readonly Mock<IZyfraDataRepository> _mockRepository;
        private readonly ZyfraDataService _service;

        public ZyfraDataServiceTests()
        {
            _mockRepository = new Mock<IZyfraDataRepository>();
            _service = new ZyfraDataService(_mockRepository.Object);
        }

        [Fact]
        public void GetZyfraData_ReturnsZyfraDataList()
        {
            // Arrange
            var mockData = new List<ZyfraData>
            {
                new ZyfraData { Id = 1, Value = 1 },
                new ZyfraData { Id = 2, Value = 2 }
            };
            _mockRepository.Setup(repo => repo.GetZyfraData()).Returns(mockData);

            // Act
            var result = _service.GetZyfraData();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Add_ReturnsAddedZyfraData()
        {
            // Arrange
            var newZyfraData = new ZyfraData { Value = 1 };
            _mockRepository.Setup(repo => repo.Add(It.IsAny<ZyfraData>()));
            _mockRepository.Setup(repo => repo.Save());

            // Act
            var result = _service.Add(newZyfraData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newZyfraData.Value, result.Value);
        }

        [Fact]
        public void Update_ReturnsTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatedZyfraData = new ZyfraData { Id = 1, Value = 2 };
            string errorMessage;

            _mockRepository.Setup(repo => repo.ZyfraDataExists(1)).Returns(true);
            _mockRepository.Setup(repo => repo.Update(It.IsAny<ZyfraData>()));

            // Act
            var result = _service.Update(1, updatedZyfraData, out errorMessage);

            // Assert
            Assert.True(result);
            Assert.Null(errorMessage);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.Delete(1));

            // Act
            _service.Delete(1);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
