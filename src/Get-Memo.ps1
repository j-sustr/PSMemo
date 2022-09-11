

function Get-Memo {
    param (
        [Parameter(Mandatory, Position = 0)]
        [ValidateNotNullOrEmpty()]
        [PSMemoKeyCompleterAttribute()]
        [string] $Key
    )

    Write-Host $Key
}
