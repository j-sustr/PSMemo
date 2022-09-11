using System.Management.Automation;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Get, "Memo")]
public class GetMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Key { get; set; }

    protected override void ProcessRecord()
    {
        WriteObject("Hello");
    }
}
