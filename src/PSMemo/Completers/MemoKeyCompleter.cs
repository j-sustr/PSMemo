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
        string pattern = KeyUtils.ConvertKeyToPath(wordToComplete);
        if (wordToComplete.EndsWith(KeyUtils.Separator))
        {
            pattern = $"{pattern}{Path.DirectorySeparatorChar}*";
        }
        else
        {
            pattern = $"{pattern}*";
        }


        Matcher matcher = new();
        matcher.AddInclude(pattern);

        return matcher.GetResultsInFullPath(Constants.PSMemoFolderPath).Select(path =>
        {
            return new CompletionResult(path, path, CompletionResultType.ParameterValue, path);
        });
    }
}