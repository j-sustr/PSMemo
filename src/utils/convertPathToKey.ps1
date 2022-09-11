

function convertPathToKey([string]$path) {
    $path = $path.Trim([IO.Path]::DirectorySeparatorChar)
    $components = $path.Split([IO.Path]::DirectorySeparatorChar)
    return [string]::Join('.', $components)
}
