using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Repository;

namespace PSMemo.Completers;

public class MemoCompleter : IArgumentCompleter
{
    public string? Key { get; init; }
    public ScriptBlock? KeyResolver { get; init; }
    public readonly IMemoRepository _repository;

    public MemoCompleter()
    {
        KeyResolver = ScriptBlock.Create(@"
            param($boundParams) $boundParams[""Key""]
        ");
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoCompleter(string key)
    {
        Key = key;

        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoCompleter(ScriptBlock keyResolver)
    {
        KeyResolver = keyResolver;

        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    // TODO: Implement when IArgumentCompleterFactory is available
    // public MemoCompleter(IMemoRepository repository)
    // {
    //     ArgumentNullException.ThrowIfNull(repository);

    //     _repository = repository;
    // }

    public IEnumerable<CompletionResult> CompleteArgument(
        string commandName,
        string parameterName,
        string wordToComplete,
        CommandAst commandAst,
        IDictionary fakeBoundParameters)
    {
        if (KeyResolver != null)
        {
            return CompleteArgumentUsingKeyResolver(KeyResolver, fakeBoundParameters);
        }
        if (Key != null)
        {
            return CompleteArgumentUsingKey(Key);
        }

        return Enumerable.Empty<CompletionResult>();
    }

    public IEnumerable<CompletionResult> CompleteArgumentUsingKey(string key)
    {
        IEnumerable<string> values;
        try
        {
            values = _repository.GetCollection(key);
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

    public IEnumerable<CompletionResult> CompleteArgumentUsingKeyResolver(ScriptBlock keyResolver, IDictionary fakeBoundParameters)
    {
        var argumentsToResolver = new object[] { fakeBoundParameters };

        Collection<PSObject>? customResults = null;
        try
        {
            customResults = keyResolver.Invoke(argumentsToResolver);
        }
        catch (System.Exception)
        {
        }

        if (customResults == null || customResults.Count == 0)
        {
            return Enumerable.Empty<CompletionResult>();
        }

        string key = customResults.First().ToString();

        if (String.IsNullOrEmpty(key))
        {
            return Enumerable.Empty<CompletionResult>();
        }


        IEnumerable<string> values;
        try
        {
            values = _repository.GetCollection(key);
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
    private readonly string? _key;
    private readonly ScriptBlock? _keyResolver;

    public MemoCompletionsAttribute(string key)
    {
        _key = key;
    }

    public MemoCompletionsAttribute(ScriptBlock keyResolver)
    {
        _keyResolver = keyResolver;
    }

    // TODO: Implement when IArgumentCompleterFactory is available
    // IArgumentCompleter Create()
    // {
    //     var repository = DefaultMemoRepositoryProvider.GetRepository();

    //     return new MemoCompleter(repository)
    //     {
    //         Key = _key,
    //         KeyResolver = _keyResolver,
    //     };
    // }
}