using System.IO.Abstractions;
using System.Management.Automation;
using PSMemo.Repository;

namespace PSMemo.Cmdlets.Common;

public abstract class PSMemoCmdlet : Cmdlet
{
    internal IMemoRepository? _repository { get; set; }

    public PSMemoCmdlet() : base() { }

    public PSMemoCmdlet(PSMemoCmdletDependencies dependencies) : base()
    {
        ArgumentNullException.ThrowIfNull(dependencies.Repository);

        _repository = dependencies.Repository;
    }

    public IMemoRepository GetRepository()
    {
        if (_repository != null)
        {
            return _repository;
        }

        return DefaultMemoRepositoryProvider.GetRepository();
    }

}