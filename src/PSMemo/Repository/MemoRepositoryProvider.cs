

using PSMemo;
using PSMemo.Repository;

public static class MemoRepositoryProvider
{
    public static IMemoRepository GetRepository()
    {
        return new MemoFileRepository(Constants.PSMemoFolderPath);
    }
}