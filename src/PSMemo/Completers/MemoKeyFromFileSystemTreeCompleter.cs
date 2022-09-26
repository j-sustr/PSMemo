using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Utils;

namespace PSMemo.Completers;

public class MemoKeyFromFileSystemTreeCompleter : IArgumentCompleter
{

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        string path = KeyUtils.ConvertKeyToPath(wordToComplete);
        string pattern = "*";
        if (!wordToComplete.EndsWith(KeyUtils.Separator))
        {
            pattern = $"{Path.GetFileName(path)}*";
            path = Path.GetDirectoryName(path) ?? "";
        }

        string keyBase = KeyUtils.ConvertPathToKey(path);
        if (!String.IsNullOrEmpty(keyBase))
        {
            keyBase = $"{keyBase}{KeyUtils.Separator}";
        }

        string fullPath = Path.Join(Constants.PSMemoFolderPath, path);
        var dir = new DirectoryInfo(fullPath);
        if (!dir.Exists)
        {
            return Enumerable.Empty<CompletionResult>();
        }

        return dir.EnumerateFileSystemInfos(pattern, SearchOption.TopDirectoryOnly).Select(fsi =>
        {
            var key = ConvertFileSystemInfoToKey(fsi, keyBase);
            return new CompletionResult(key, key, CompletionResultType.ParameterValue, key);
        });
    }

    private string ConvertFileSystemInfoToKey(FileSystemInfo fsi, string keyBase)
    {

        if ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
        {
            return $"{keyBase}{fsi.Name}{KeyUtils.Separator}";
        }
        else
        {
            var fileName = Path.GetFileNameWithoutExtension(fsi.Name);
            return $"{keyBase}{fileName}";
        }
    }
}