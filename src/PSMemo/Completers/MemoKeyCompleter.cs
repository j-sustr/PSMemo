using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.Extensions.FileSystemGlobbing;
using PSMemo.Utils;

namespace PSMemo.Completers;

public class MemoKeyCompleter : IArgumentCompleter
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

        var dir = new DirectoryInfo(Constants.PSMemoFolderPath);
        if (!dir.Exists)
        {
            return Enumerable.Empty<CompletionResult>();
        }

        return dir.EnumerateFileSystemInfos(pattern, SearchOption.TopDirectoryOnly).Select(info =>
        {
            var path = $"{info.Name}{KeyUtils.Separator}";
            return new CompletionResult(path, path, CompletionResultType.ParameterValue, path);
        });
    }
}