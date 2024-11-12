using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Collections.Generic;
using Xunit;

public class UniTest1
{
    private readonly Mock<IFileService> _fileServiceMock;

    public UniTest1()
    {
        _fileServiceMock = new Mock<IFileService>();
        Program.SetFileService(_fileServiceMock.Object);
    }

    [Fact]
    public void LoadData_ShouldReturnEmptyDictionary_WhenFileDoesNotExist()
    {
        string filename = "data.txt";
        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(false);

        var result = Program.Load(filename);

        Assert.Empty(result);
    }

    [Fact]
    public void LoadData_ShouldReturnData_WhenFileExists()
    {
        string filename = "data.txt";
        var lines = new List<string> { "1=1", "2=2" };
        _fileServiceMock.Setup(fs => fs.Exists(filename)).Returns(true);
        _fileServiceMock.Setup(fs => fs.ReadLines(filename)).Returns(lines);

        var result = Program.Load(filename);

        Assert.Equal(2, result.Count);
        Assert.Equal("1", result["1"]);
        Assert.Equal("2", result["2"]);
    }

    [Fact]
    public void SaveData_ShouldWriteDataToFile()
    {
        string filename = "data.txt";
        var data = new Dictionary<string, string>
        {
            { "1", "1" },
            { "2", "2" }
        };

        Program.Save(filename, data);

        _fileServiceMock.Verify(fs => fs.WriteLines(filename, It.IsAny<IEnumerable<string>>()), Times.Once);
    }

    [Fact]
    public void CreateInitialData_ShouldWriteInitialDataToFile()
    {
        string filename = "data.txt";

        Program.Create(filename);

        _fileServiceMock.Verify(fs => fs.WriteLines(filename, It.IsAny<IEnumerable<string>>()), Times.Once);
    }
}
