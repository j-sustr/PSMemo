using namespace System.Collections
using namespace System.Collections.Generic
using namespace System.Management.Automation
using namespace System.Management.Automation.Language


class PSMemoKeyCompleterAttribute : System.Management.Automation.ArgumentCompleterAttribute {

    [string]$Root

    PSMemoKeyCompleterAttribute() : base([PSMemoKeyCompleterAttribute]::_createScriptBlock($this)) {}

    hidden static [ScriptBlock] _createScriptBlock([PSMemoKeyCompleterAttribute] $instance) {
        $scriptblock = {
            param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameters)

            $root = $instance.Root ?? (getPSMemoHome)

            $childPathToComplete = convertKeyToPath $wordToComplete
            $pathToComplete = Join-Path $root $childPathToComplete

            return Convert-Path "$pathToComplete*" | ForEach-Object {
                $key = convertPathToKey $_.Replace($root, '')
                return [CompletionResult]::new($key, $key, 'ParameterValue', $key)
            }
        }.GetNewClosure()
        return $scriptblock
    }
}
