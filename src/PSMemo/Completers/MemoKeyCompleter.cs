using System.Collections;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Text.RegularExpressions;
using PSMemo.Repository;
using PSMemo.Utils;

namespace PSMemo.Completers;

public class MemoKeyCompleter : IArgumentCompleter
{
    private readonly IMemoRepository _repository;

    public MemoKeyCompleter()
    {
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoKeyCompleter(IMemoRepository repository)
    {
        _repository = repository;
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

        return _repository.ListCollectionKeys().Where(key =>
        {
            return pattern.IsMatch(key);
        }).Select(key =>
        {
            return new CompletionResult(key, key, CompletionResultType.ParameterValue, key);
        });
    }
}

public class MemoKeyCompletionsAttribute
{
    public MemoKeyCompletionsAttribute()
    {

    }

    IArgumentCompleter Create()
    {
        return new MemoKeyCompleter();
    }
}