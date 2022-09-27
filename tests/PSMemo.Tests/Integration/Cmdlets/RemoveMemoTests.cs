using Moq;
using PSMemo.Cmdlets.Common;
using PSMemo.Repository;
using System.Linq;
using PSMemo.Tests;
using PSMemo.Cmdlets;
using static PSMemo.Tests.TestUtils;
using System.IO.Abstractions;
using FluentAssertions;
using System.Management.Automation;

namespace PSMemo.Tests.Integration.Cmdlets;

public class RemoveMemoTests
{
    [Fact]
    public void RemoveOneItem()
    {
        var mockRuntime = new MockCommandRuntime<string>();
        var mockFileSystem = new Mock<IFileSystem>();
        var repo = new MemoFileSystemRepository(mockFileSystem.Object, "X:\\test");

        string[]? writtenLines = null;

        mockFileSystem.Setup(x => x.File.Exists(It.IsAny<string>()))
            .Returns(true);
        mockFileSystem.Setup(x => x.File.ReadAllLines(It.IsAny<string>()))
            .Returns(new string[] { "item1", "item2" });
        mockFileSystem.Setup(x => x.File.WriteAllLines(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
            .Callback((string _, IEnumerable<string> _lines) => writtenLines = _lines.ToArray());


        var cmdlet = new RemoveMemo()
        {
            CommandRuntime = mockRuntime,
            Repository = repo,
            Key = "xyz",
            Value = "item2"
        };

        Execute(cmdlet);

        string expectedPath = @"X:\test\xyz.memo";
        mockFileSystem.Verify(x => x.File.ReadAllLines(expectedPath), Times.Once);
        mockFileSystem.Verify(x => x.File.WriteAllLines(expectedPath, It.IsAny<IEnumerable<string>>()), Times.Once);

        mockRuntime.Output.Should().HaveCount(0);

        writtenLines.Should().Equal("item1");
    }

    [Fact]
    public void RemoveCollection()
    {
        var mockRuntime = new MockCommandRuntime<string>();
        var mockFileSystem = new Mock<IFileSystem>();
        var repo = new MemoFileSystemRepository(mockFileSystem.Object, "X:\\test");

        mockFileSystem.Setup(x => x.File.Exists(It.IsAny<string>()))
            .Returns(true);


        var cmdlet = new RemoveMemo()
        {
            CommandRuntime = mockRuntime,
            Repository = repo,
            Key = "xyz",
            RemoveCollection = new SwitchParameter(true)
        };

        Execute(cmdlet);

        string expectedPath = @"X:\test\xyz.memo";
        mockFileSystem.Verify(x => x.File.Exists(expectedPath), Times.Once);
        mockFileSystem.Verify(x => x.File.Delete(expectedPath), Times.Once);

        mockRuntime.Output.Should().HaveCount(0);
    }
}