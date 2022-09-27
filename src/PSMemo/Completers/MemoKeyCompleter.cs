using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Text.RegularExpressions;
using PSMemo.Repository;
using PSMemo.Utils;

namespace PSMemo.Completers;

public class MemoKeyCompleter : IArgumentCompleter
{

    public MemoKeyCompleter()
    {

    }

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        wordToComplete = wordToComplete.Trim();

        if (!wordToComplete.EndsWith("*"))
        {
            wordToComplete = $"{wordToComplete}*";
        }

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

public class MemoKeyCompletionsAttribute : ArgumentCompleterAttribute
{
    public MemoKeyCompletionsAttribute() : base(typeof(MemoKeyCompleter))
    {

    }

}