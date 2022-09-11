using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;

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

        string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };

        foreach (var s in cars)
        {
            yield return new CompletionResult(s, s, CompletionResultType.ParameterValue, s);
        }
    }
}