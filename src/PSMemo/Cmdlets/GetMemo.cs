using System.Management.Automation;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Get, "Memo")]
public class GetMemo : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    protected override void ProcessRecord()
    {
        var repo = MemoRepositoryProvider.GetRepository();

        var values = repo.GetAll(Key);

        WriteObject(values, true);
    }
}
