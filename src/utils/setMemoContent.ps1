

function setMemoContent([string]$path, [hashtable]$memo) {
    $memo.Keys | Sort-Object | Set-Content -Path $path -Encoding Default
}
