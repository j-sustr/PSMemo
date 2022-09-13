

using PSMemo;
using PSMemo.Repository;

public static class MemoRepositoryProvider
{
    public static IMemoRepository Get()
    {
        return new MemoFileRepository(Constants.PSMemoFolderPath);
    }
}