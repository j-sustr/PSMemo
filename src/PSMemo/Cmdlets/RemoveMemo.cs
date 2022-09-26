using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Remove, "Memo")]
public class RemoveMemo : PSMemoCmdlet
{
    const string psDefault = "Default";
    const string psRemoveCollection = "RemoveCollection";

    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyFromFileSystemTreeCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    [Parameter(Position = 1, ParameterSetName = psDefault)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    [Parameter(ParameterSetName = psRemoveCollection)]
    public SwitchParameter RemoveCollection { get; set; }

    protected override void ProcessRecord()
    {
        var repo = GetRepository();

        if (RemoveCollection.IsPresent)
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