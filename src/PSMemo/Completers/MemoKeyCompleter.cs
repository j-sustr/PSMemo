using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Text.RegularExpressions;
using PSMemo.Repository;
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
        var pattern = new WildcardPattern(wordToComplete, WildcardOptions.IgnoreCase);

        var repo = DefaultMemoRepositoryProvider.GetRepository();

        return repo.ListCollectionKeys().Where(key =>
        {
            return pattern.IsMatch(key);
        }).Select(key =>
        {
            return new CompletionResult(key, key, CompletionResultType.ParameterValue, key);
        });
    }
}