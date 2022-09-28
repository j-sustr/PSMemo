using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsDiagnostic.Test, "MemoCompleter")]
public class TestMemoCompleter : PSMemoCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ValidateNotNullOrEmpty]
    [ArgumentCompleter(typeof(MemoCompleter))]
    public string Key { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    [ArgumentCompleter(typeof(MemoValueByDynamicKeyCompleter))]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    protected override void ProcessRecord()
    {
        WriteObject($"Key '{Key}', value '{Value}'");
    }
}