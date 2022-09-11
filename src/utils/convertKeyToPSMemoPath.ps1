

function convertKeyToPSMemoPath([string]$key) {
    $components = $key.Split('.')
    $childPath = [IO.Path]::Combine($components)
    $childPath = '{0}.memo' -f $childPath

    return Join-Path (getPSMemoHome) $childPath
}
