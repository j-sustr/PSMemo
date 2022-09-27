
using FluentAssertions;
using static PSMemo.Utils.PathUtils;

namespace PSMemo.Tests.Unit;

public class PathUtilsTests
{
    [Theory]
    [InlineData(@"ab\cd.ext")]
    public void FileNameContainsInvalidChars(string fileName)
    {
        bool result = ContainsInvalidFileNameChars(fileName);

        result.Should().BeTrue();
    }
}