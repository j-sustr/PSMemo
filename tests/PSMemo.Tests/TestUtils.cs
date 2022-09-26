using System.Management.Automation;

namespace PSMemo.Tests;

public static class TestUtils
{
    public static void Execute(Cmdlet cmdlet)
    {
        foreach (var _ in cmdlet.Invoke()) { }
    }
}