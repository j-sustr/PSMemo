

function convertKeyToPSMemoPath([string]$key) {
    $components = $key.Split('.')
    $childPath = [IO.Path]::Combine($components)

    return Join-Path (getPSMemoHome) $childPath
}
