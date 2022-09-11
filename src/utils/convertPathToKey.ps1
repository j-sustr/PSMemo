

function convertPathToKey([string]$path) {
    $components = $path.Split([IO.Path]::DirectorySeparatorChar)
    return [string]::Join('.', $components)
}
