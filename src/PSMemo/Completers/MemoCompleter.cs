using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Repository;

namespace PSMemo.Completers;

public class MemoCompleter : IArgumentCompleter
{
    public readonly string _keyParameter;
    public readonly IMemoRepository _repository;

    public MemoCompleter()
    {
        _keyParameter = "Key";
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoCompleter(string keyParameter, IMemoRepository repository)
    {
        _keyParameter = keyParameter;
        _repository = repository;
    }

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        string? key = fakeBoundParameters[_keyParameter] as string;
        if (String.IsNullOrWhiteSpace(key))
        {
            return Enumerable.Empty<CompletionResult>();
        }

        IEnumerable<string> values;
        try
        {
            values = _repository.GetCollection(key as string);
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
    private readonly string _keyParameter;

    public MemoCompletionsAttribute(string keyParameter)
    {
        _keyParameter = keyParameter;
    }

    IArgumentCompleter Create()
    {
        var repository = DefaultMemoRepositoryProvider.GetRepository();

        return new MemoCompleter(_keyParameter, repository);
    }
}