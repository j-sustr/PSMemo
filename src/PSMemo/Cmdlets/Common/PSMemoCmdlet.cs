using System.IO.Abstractions;
using System.Management.Automation;
using PSMemo.Repository;

namespace PSMemo.Cmdlets.Common;

public abstract class PSMemoCmdlet : Cmdlet
{
    private IMemoRepository? _repository;

    public IMemoRepository Repository
    {
        set
        {
            if (_repository != null)
            {
                throw new InvalidOperationException();
            }

            ArgumentNullException.ThrowIfNull(value);

            _repository = value;
        }
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