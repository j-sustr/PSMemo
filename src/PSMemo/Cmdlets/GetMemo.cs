using System.Management.Automation;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Get, "Memo")]
public class GetMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    public string Key { get; set; }

    protected override void ProcessRecord()
    {
        WriteObject($"Entered key: {Key}");
    }
}
