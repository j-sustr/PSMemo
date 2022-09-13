using System.Management.Automation;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Remove, "Memo")]
public class RemoveMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; }

    [Parameter(Position = 1)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; }

    protected override void ProcessRecord()
    {


        WriteObject($"Entered key: {Key}");
    }
}