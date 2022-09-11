

function convertKeyToPSMemoPath([string]$key) {
    $childPath = convertKeyToPath $key
    $childPath = '{0}.memo' -f $childPath
    return Join-Path (getPSMemoHome) $childPath
}
