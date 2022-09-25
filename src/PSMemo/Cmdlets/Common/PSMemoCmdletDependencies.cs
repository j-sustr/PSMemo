using PSMemo.Repository;

namespace PSMemo.Cmdlets.Common;

public struct PSMemoCmdletDependencies
{
    public IMemoRepository Repository { get; set; }
}