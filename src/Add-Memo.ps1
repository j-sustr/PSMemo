

function Add-Memo {
    param (
        [Parameter(Mandatory, Position = 0)]
        [ValidateNotNullOrEmpty()]
        [string] $Key,

        [Parameter(Mandatory, Position = 1)]
        [ValidateNotNullOrEmpty()]
        [string] $Value
    )

    $memoPath = convertKeyToPSMemoPath $Key

    if (-not (Test-Path $memoPath)) {
        Write-Verbose "Creating new memo file '$memoPath'"
        New-Item -Path $memoPath -ItemType File -Force
    }

    $memo = getMemoContent $memoPath
    $memo[$key] = $true;
    setMemoContent $memoPath $memo
}
