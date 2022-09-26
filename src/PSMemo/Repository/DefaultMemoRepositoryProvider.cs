using System.IO.Abstractions;

namespace PSMemo.Repository;

public static class DefaultMemoRepositoryProvider
{
    public static IMemoRepository GetRepository()
    {
        var fileSystem = new FileSystem();

        return new MemoFileSystemRepository(fileSystem, Constants.PSMemoFolderPath);
    }
}