using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Remove, "Memo")]
public class RemoveMemo : PSMemoCmdlet
{
    const string psValue = "Value";
    const string psBranch = "Branch";

    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyFromFileSystemTreeCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    [Parameter(Position = 1, ParameterSetName = psValue)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    [Parameter(ParameterSetName = psBranch)]
    public SwitchParameter Branch { get; set; }

    protected override void ProcessRecord()
    {
        var repo = GetRepository();

        if (Branch.IsPresent)
        {
            repo.RemoveCollection(Key);
            return;
        }

        bool ok = repo.TryRemove(Key, Value);
        if (ok)
        {
            WriteVerbose($"Memo '{Value}' was removed from '{Key}'.");
        }
        else
        {
            WriteWarning($"Memo '{Value}' does not exist in '{Key}'.");
        }
    }
}