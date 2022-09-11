using namespace System.Collections
using namespace System.Collections.Generic
using namespace System.Management.Automation
using namespace System.Management.Automation.Language


class PSMemoKeyCompleterAttribute : System.Management.Automation.ArgumentCompleterAttribute {

    [string]$Root = $null

    PSMemoKeyCompleterAttribute() : base([PSMemoKeyCompleterAttribute]::_createScriptBlock($this)) {}

    hidden static [ScriptBlock] _createScriptBlock([PSMemoKeyCompleterAttribute] $instance) {
        $scriptblock = {
            param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameters)

            # if ([string]::IsNullOrEmpty($instance.Root)) {
            #     $instance.Root = Get-PSMemoHome
            # }

            return @('aaa', 'bbb') | ForEach-Object {
                return [CompletionResult]::new($_, $_, 'ParameterValue', $_)
            }

            # $childPathToComplete = convertKeyToPath $wordToComplete
            # $pathToComplete = Join-Path $instance.Root $childPathToComplete

            # return Convert-Path "$pathToComplete*" | ForEach-Object {
            #     $key = convertPathToKey $_.Replace($instance.Root, '')
            #     $key = "$key."
            #     return [CompletionResult]::new($key, $key, 'ParameterValue', $key)
            # }
        }.GetNewClosure()
        return $scriptblock
    }
}
