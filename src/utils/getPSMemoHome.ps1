

$global:PSMemoHome = "$HOME\psmemo"

function getPSMemoHome {
    if (-not (Test-Path $global:PSMemoHome)) {
        $null = New-Item -Path $global:PSMemoHome -ItemType Directory
    }
    return $global:PSMemoHome
}
