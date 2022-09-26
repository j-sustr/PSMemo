using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Add, "Memo")]
public class AddMemo : PSMemoCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyFromFileSystemTreeCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    [Parameter(Mandatory = true, Position = 1)]
    [ValidateNotNullOrEmpty]
    public string Value { get; set; } = null!;

    public AddMemo() : base() { }

    public AddMemo(PSMemoCmdletDependencies dependencies) : base(dependencies) { }

    protected override void ProcessRecord()
    {
        var repo = GetRepository();

        bool ok = repo.TryAdd(Key, Value);
        if (ok)
        {
            WriteVerbose($"Memo '{Value}' was added.");
        }
        else
        {
            WriteWarning($"Memo '{Value}' is already added.");
        }

    }
}