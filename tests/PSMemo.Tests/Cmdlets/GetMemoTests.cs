using Moq;
using PSMemo.Cmdlets.Common;
using PSMemo.Repository;
using System.Linq;
using PSMemo.Tests;
using PSMemo.Cmdlets;
using static PSMemo.Tests.TestUtils;

namespace PSMemo.Tests.Cmdlets;

public class GetMemoTests
{
    [Fact]
    public void SuccessfullyGet()
    {
        var mockRuntime = new MockCommandRuntime<string>();
        var mockRepo = new Mock<IMemoRepository>();

        mockRepo.Setup(x => x.GetAll(It.IsAny<string>()))
            .Returns(new string[] { "item1", "item2" });

        var deps = new PSMemoCmdletDependencies
        {
            Repository = mockRepo.Object
        };
        var cmdlet = new GetMemo(deps)
        {
            CommandRuntime = mockRuntime,
            Key = "a.b.c"
        };

        Execute(cmdlet);

        var output = mockRuntime.Output;
        Assert.Equal(2, output.Count);
        Assert.Equal(output[0], "item1");
        Assert.Equal(output[1], "item2");
    }
}