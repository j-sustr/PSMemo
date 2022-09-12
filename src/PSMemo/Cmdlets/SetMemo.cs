using System.Management.Automation;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Set, "Memo")]
public class SetMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ValidateNotNullOrEmpty]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    public string Key { get; set; }

    [Parameter(Mandatory = true, Position = 1)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; }

    protected override void ProcessRecord()
    {
        WriteObject($"Entered key: {Key}");
    }
}