

function getMemoContent([string]$path) {
    $memo = @{}

    Get-Content -Path $path -Encoding Default |
    ForEach-Object { $_.Trim() } |
    Where-Object { ![string]::IsNullOrEmpty($_) } |
    ForEach-Object {
        $memo[$_] = $true
    }

    return $memo
}
