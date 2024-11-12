using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZyfraServer.Controllers;
using ZyfraServer.Interfaces;

public class UniTest1
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly FileController _fileController;

    public UniTest1()
    {
        _fileServiceMock = new Mock<IFileService>();
        _fileController = new FileController(_fileServiceMock.Object);
    }

    [Fact]
    public void Get_ShouldReturnNotFound_WhenFileDoesNotExist()
    {
        string filename = "data.txt";
        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(false);

        var result = _fileController.Get(filename);

        Assert.IsType<NotFoundObjectResult>(result.Result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.Equal("Файл не найден или пуст.", notFoundResult.Value);
    }

    [Fact]
    public void Get_ShouldReturnData_WhenFileExists()
    {
        string filename = "data.txt";
        var lines = new List<string> { "1=1", "2=2" };
        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(true);
        _fileServiceMock.Setup(fs => fs.ReadLines(filename)).Returns(lines);

        var result = _fileController.Get(filename);

        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        var data = okResult.Value as Dictionary<string, string>;
        Assert.Equal(2, data.Count);
        Assert.Equal("1", data["1"]);
        Assert.Equal("2", data["2"]);
    }

    [Fact]
    public void Update_ShouldReturnNotFound_WhenKeyDoesNotExist()
    {
        string filename = "data.txt";
        var entry = new KeyValuePair<string, string>("1", "new value");
        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(true);
        _fileServiceMock.Setup(fs => fs.ReadLines(filename)).Returns(new List<string> { "2=2" });

        var result = _fileController.UpdateFile(filename, entry);

        Assert.IsType<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.Equal($"Id '{entry.Key}' не найден.", notFoundResult.Value);
    }

    [Fact]
    public void Update_ShouldUpdateData_WhenKeyExists()
    {
        string filename = "data.txt";
        var data = new Dictionary<string, string> { { "1", "1" }, { "2", "2" } };
        var entry = new KeyValuePair<string, string>("1", "new value");

        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(true);
        _fileServiceMock.Setup(fs => fs.ReadLines(filename)).Returns(new List<string> { "1=1", "2=2" });

        var result = _fileController.UpdateFile(filename, entry);

        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal($"Обновлено: {entry.Key} = {entry.Value}", okResult.Value);

        _fileServiceMock.Verify(fs => fs.WriteLines(filename, It.IsAny<IEnumerable<string>>()), Times.Once);
    }

    [Fact]
    public void Create_ShouldWriteInitialDataToFile()
    {
        string filename = "data.txt";

        var result = _fileController.CreateFile(filename);

        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal("Файл создан с начальными данными.", okResult.Value);

        _fileServiceMock.Verify(fs => fs.WriteLines(filename, It.IsAny<IEnumerable<string>>()), Times.Once);
    }
}