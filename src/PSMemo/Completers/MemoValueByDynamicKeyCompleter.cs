using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using PSMemo.Repository;

namespace PSMemo.Completers;

public class MemoValueByDynamicKeyCompleter : IArgumentCompleter
{
    public readonly string _keyParameter;
    public readonly IMemoRepository _repository;

    public MemoValueByDynamicKeyCompleter()
    {
        _keyParameter = "Key";
        _repository = DefaultMemoRepositoryProvider.GetRepository();
    }

    public MemoValueByDynamicKeyCompleter(string keyParameter, IMemoRepository? repository)
    {
        _keyParameter = keyParameter;
        _repository = repository ?? DefaultMemoRepositoryProvider.GetRepository();
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