using System.Management.Automation;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Set, "Memo")]
public class SetMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    protected override void ProcessRecord()
    {
        WriteObject($"Entered key: {Key}");
    }
}