using Moq;
using PSMemo.Repository;

namespace PSMemo.Tests;

public class GetMemoTests
{
    [Fact]
    public void SuccessfullyGet(string parameter)
    {
        var mockRepo = new Mock<IMemoRepository>();
    }
}