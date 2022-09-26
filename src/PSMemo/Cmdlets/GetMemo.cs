using System.Management.Automation;
using PSMemo.Cmdlets.Common;
using PSMemo.Completers;

namespace PSMemo.Cmdlets;

[Cmdlet(VerbsCommon.Get, "Memo")]
public class GetMemo : PSMemoCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    [ArgumentCompleter(typeof(MemoKeyCompleter))]
    [ValidateNotNullOrEmpty]
    public string Key { get; set; } = null!;

    public GetMemo() : base() { }

    public GetMemo(PSMemoCmdletDependencies dependencies) : base(dependencies) { }

    protected override void ProcessRecord()
    {
        var repo = GetRepository();

        var values = repo.GetAll(Key);

        WriteObject(values, true);
    }
}
