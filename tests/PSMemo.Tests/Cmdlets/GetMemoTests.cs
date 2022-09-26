using Moq;
using PSMemo.Cmdlets.Common;
using PSMemo.Repository;
using System.Linq;

namespace PSMemo.Cmdlets.Tests;

public class GetMemoTests
{
    [Fact]
    public void SuccessfullyGet()
    {
        var mockRepo = new Mock<IMemoRepository>();

        mockRepo.Setup(x => x.GetAll(It.IsAny<string>()))
            .Returns(new string[] { "item1", "item2" });

        var deps = new PSMemoCmdletDependencies
        {
            Repository = mockRepo.Object
        };
        var cmdlet = new GetMemo(deps)
        {
            Key = "a.b.c"
        };

        var result = cmdlet.Invoke().OfType<string>().ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(result[0], "item1");
        Assert.Equal(result[1], "item2");
    }
}