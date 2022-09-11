using namespace System.Collections
using namespace System.Collections.Generic
using namespace System.Management.Automation
using namespace System.Management.Automation.Language


class PSMemoKeyCompleterAttribute : System.Management.Automation.ArgumentCompleterAttribute {

    [string]$Root

    PSMemoKeyCompleterAttribute() : base([PSMemoKeyCompleterAttribute]::_createScriptBlock($this)) {
        $this.Root = getPSMemoHome
    }

    hidden static [ScriptBlock] _createScriptBlock([PSMemoKeyCompleterAttribute] $instance) {
        $scriptblock = {
            param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameters)

            return @('abc', 'xyz') | ForEach-Object {
                return [CompletionResult]::new($_, $_, 'ParameterValue', $_)
            }
        }.GetNewClosure()
        return $scriptblock
    }
}
