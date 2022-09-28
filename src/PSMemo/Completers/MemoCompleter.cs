using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Repository;

namespace PSMemo.Completers;

public class MemoCompleter : IArgumentCompleter
{
    public readonly string _key;
    public readonly IMemoRepository _repository;

    public MemoCompleter()
    {
        _key = "test1";
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoCompleter(string key)
    {
        _key = key;
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoCompleter(string key, IMemoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _key = key;
        _repository = repository;
    }

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        IEnumerable<string> values;
        try
        {
            values = _repository.GetCollection(_key);
        }
        catch (System.Exception)
        {
            return Enumerable.Empty<CompletionResult>();
        }

        return values
        .Select(value =>
        {
            return new CompletionResult(value, value, CompletionResultType.ParameterValue, value);
        });
    }
}

public class MemoCompletionsAttribute
{
    private readonly string _key;

    public MemoCompletionsAttribute(string key)
    {
        _key = key;
    }

    IArgumentCompleter Create()
    {
        var repository = DefaultMemoRepositoryProvider.GetRepository();

        return new MemoCompleter(_key, repository);
    }
}