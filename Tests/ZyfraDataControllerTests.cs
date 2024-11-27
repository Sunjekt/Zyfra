using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using ZyfraServer.Controllers;
using ZyfraServer.Models;
using ZyfraServer.Intefaces.Services;

namespace ZyfraServer.Tests
{
    public class ZyfraDataControllerTests
    {
        private readonly Mock<IZyfraDataService> _mockService;
        private readonly ZyfraDataController _controller;

        public ZyfraDataControllerTests()
        {
            _mockService = new Mock<IZyfraDataService>();
            _controller = new ZyfraDataController(_mockService.Object);
        }

        [Fact]
        public async Task GetZyfraData_ReturnsOkResult_WithZyfraDataList()
        {
            // Arrange
            var mockData = new List<ZyfraData>
            {
                new ZyfraData { Id = 1, Value = 1 },
                new ZyfraData { Id = 2, Value = 2 }
            };
            _mockService.Setup(service => service.GetZyfraData()).Returns(mockData);

            // Act
            var result = await _controller.GetZyfraData();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnData = Assert.IsAssignableFrom<IEnumerable<ZyfraData>>(okResult.Value);
            Assert.Equal(2, returnData.Count());
        }

        [Fact]
        public async Task GetZyfraDataById_ReturnsOkResult_WithZyfraData()
        {
            // Arrange
            var mockData = new ZyfraData { Id = 1, Value = 1 };
            _mockService.Setup(service => service.GetZyfraDataById(1)).Returns(mockData);

            // Act
            var result = await _controller.GetZyfraData(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnData = Assert.IsType<ZyfraData>(okResult.Value);
            Assert.Equal(mockData.Id, returnData.Id);
            Assert.Equal(mockData.Value, returnData.Value);
        }

        [Fact]
        public async Task GetZyfraDataById_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetZyfraDataById(1)).Returns((ZyfraData)null);

            // Act
            var result = await _controller.GetZyfraData(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostZyfraData_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newZyfraData = new ZyfraData { Id = 1, Value = 1 };
            _mockService.Setup(service => service.Add(It.IsAny<ZyfraData>())).Returns(newZyfraData);

            // Act
            var result = await _controller.PostZyfraData(newZyfraData);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetZyfraData", createdResult.ActionName);
            Assert.Equal(newZyfraData.Id, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task PutZyfraData_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatedZyfraData = new ZyfraData { Id = 1, Value = 2 };
            string errorMessage;

            _mockService.Setup(service => service.Update(1, updatedZyfraData, out errorMessage)).Returns(true);

            // Act
            var result = await _controller.PutZyfraData(1, updatedZyfraData);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteZyfraData_ReturnsNoContent()
        {
            // Arrange
            _mockService.Setup(service => service.Delete(1));

            // Act
            var result = await _controller.DeleteZyfraData(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
