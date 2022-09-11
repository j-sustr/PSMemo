
function Remove-Memo {
    param (
        [Parameter(Mandatory, Position = 0)]
        [ValidateNotNullOrEmpty()]
        [string] $Key,

        [Parameter(Mandatory, Position = 1)]
        [ValidateNotNullOrEmpty()]
        [string] $Value
    )

    $memoPath = convertKeyToPSMemoPath $Key

    if (!(Test-Path $memoPath)) {
        throw "Memo '$memoPath' does not exist."
    }

    $memo = getMemoContent $memoPath

    if ($memo.ContainsKey()) {
        $memo[$Value].Remove($value)
        setMemoContent $memoPath $memo
    } else {
        Write-Error "Memo '$memoPath' does not contain value '$value'."
    }
}
