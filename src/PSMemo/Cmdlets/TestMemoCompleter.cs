using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsDiagnostic.Test, "MemoCompleter")]
public class TestMemoCompleter : PSMemoCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    [ArgumentCompleter(typeof(MemoCompleter))]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    protected override void ProcessRecord()
    {
        WriteObject($"Key '{Key}', value '{Value}'");
    }
}