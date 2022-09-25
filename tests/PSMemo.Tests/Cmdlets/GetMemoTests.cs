using Moq;
using PSMemo.Repository;

namespace PSMemo.Cmdlets.Tests;

public class GetMemoTests
{
    [Fact]
    public void SuccessfullyGet(string parameter)
    {
        var mockRepo = new Mock<IMemoRepository>();

        var cmdlet = new GetMemo()
        {

        }

    }
}