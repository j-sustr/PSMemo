using Moq;
using PSMemo.Cmdlets.Common;
using PSMemo.Repository;
using System.Linq;
using PSMemo.Tests;
using PSMemo.Cmdlets;
using static PSMemo.Tests.TestUtils;
using FluentAssertions;
using System.IO.Abstractions;
using static FluentAssertions.FluentActions;
using PSMemo.Exception;

namespace PSMemo.Tests.Integration.Cmdlets;

public class GetMemoTests
{
    [Fact]
    public void GetOneItem()
    {
        var mockRuntime = new MockCommandRuntime<string>();
        var mockRepo = new Mock<IMemoRepository>();

        mockRepo.Setup(x => x.GetCollection(It.IsAny<string>()))
            .Returns(new string[] { "item1", "item2" });

        var cmdlet = new GetMemo()
        {
            CommandRuntime = mockRuntime,
            Repository = mockRepo.Object,
            Key = "a.b.c"
        };

        Execute(cmdlet);

        var output = mockRuntime.Output;
        Assert.Equal(2, output.Count);
        Assert.Equal(output[0], "item1");
        Assert.Equal(output[1], "item2");
    }

    [Fact]
    public void TryGetOneItemByInvalidKey()
    {
        var mockRuntime = new MockCommandRuntime<string>();
        var mockFileSystem = new Mock<IFileSystem>();
        var repo = new MemoFileSystemRepository(mockFileSystem.Object, "X:\\test");

        mockFileSystem.Setup(x => x.File.ReadAllLines(It.IsAny<string>()))
            .Throws(new FileNotFoundException());

        var cmdlet = new GetMemo()
        {
            CommandRuntime = mockRuntime,
            Repository = repo,
            Key = "jkl"
        };

        Invoking(() => Execute(cmdlet))
            .Should().Throw<InvalidMemoKeyException>()
            .WithMessage("Memo key 'jkl' is invalid");

        mockRuntime.Errors.Should().HaveCount(0);
        mockRuntime.Output.Should().HaveCount(0);
    }
}