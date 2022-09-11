

function Get-PSMemoHome {
    $PSMemoHome = "$HOME\psmemo"

    if (-not (Test-Path $PSMemoHome)) {
        $null = New-Item -Path $PSMemoHome -ItemType Directory
    }

    return $PSMemoHome
}
