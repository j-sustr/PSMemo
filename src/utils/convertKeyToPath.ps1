

function convertKeyToPath([string]$key) {
    $components = $key.Split('.')
    return [IO.Path]::Combine($components)
}
