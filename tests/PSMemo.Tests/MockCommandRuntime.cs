using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace PSMemo.Tests;

public class MockCommandRuntime<T> : ICommandRuntime
{
    public List<T> Output
    {
        get { return _output.Cast<T>().ToList(); }
    }

    public List<ErrorRecord> Errors { get; }

    public List<string> Warnings { get; }

    public PSTransactionContext CurrentPSTransaction => throw new NotImplementedException();

    public PSHost Host => throw new NotImplementedException();

    private readonly List<object> _output;

    public MockCommandRuntime()
    {
        _output = new List<object>();
        Errors = new List<ErrorRecord>();
        Warnings = new List<string>();
    }

    public void WriteError(ErrorRecord errorRecord)
    {
        Errors.Add(errorRecord);
    }

    public void WriteWarning(string text)
    {
        Warnings.Add(text);
    }

    public void WriteObject(object sendToPipeline)
    {
        _output.Add(sendToPipeline);
    }

    public void WriteObject(object sendToPipeline, bool enumerateCollection)
    {
        if (enumerateCollection)
        {
            IEnumerator e = LanguagePrimitives.GetEnumerator(sendToPipeline);
            if (e == null)
            {
                _output.Add(sendToPipeline);
            }
            else
            {
                while (e.MoveNext())
                {
                    _output.Add(e.Current);
                }
            }
        }
        else
        {
            _output.Add(sendToPipeline);
        }
    }

    public bool ShouldContinue(string query, string caption)
    {
        throw new NotImplementedException();
    }

    public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll)
    {
        throw new NotImplementedException();
    }

    public bool ShouldProcess(string target)
    {
        throw new NotImplementedException();
    }

    public bool ShouldProcess(string target, string action)
    {
        throw new NotImplementedException();
    }

    public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption)
    {
        throw new NotImplementedException();
    }

    public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out ShouldProcessReason shouldProcessReason)
    {
        throw new NotImplementedException();
    }

    [DoesNotReturn]
    public void ThrowTerminatingError(ErrorRecord errorRecord)
    {
        throw new NotImplementedException();
    }

    public bool TransactionAvailable()
    {
        throw new NotImplementedException();
    }

    public void WriteCommandDetail(string text)
    {
        throw new NotImplementedException();
    }

    public void WriteDebug(string text)
    {
        throw new NotImplementedException();
    }

    public void WriteProgress(long sourceId, ProgressRecord progressRecord)
    {
        throw new NotImplementedException();
    }

    public void WriteProgress(ProgressRecord progressRecord)
    {
        throw new NotImplementedException();
    }

    public void WriteVerbose(string text)
    {
        throw new NotImplementedException();
    }
}