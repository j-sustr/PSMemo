using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Repository;

namespace PSMemo.Completers;

public class MemoCompleter : IArgumentCompleter
{
    public string Key { get; set; }

    public MemoCompleter(string key)
    {
        Key = key;
    }

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        var repo = DefaultMemoRepositoryProvider.GetRepository();

        var values = repo.GetCollection(Key);

        return values
        .Where(value => value.StartsWith(wordToComplete))
        .Select(value =>
        {
            return new CompletionResult(value, value, CompletionResultType.ParameterValue, value);
        });
    }
}