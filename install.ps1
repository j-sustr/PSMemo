$SrcRoot = "$PSScriptRoot\src\PSMemo"
$ModuleRoot = "$PSScriptRoot\src\PSMemo\bin\Debug\net6.0\publish"

'Running dotnet publish...'
dotnet publish $SrcRoot -c Release

$psModulePath = Get-PSModulePathForCurrentUser
$manifestPath = Convert-Path $ModuleRoot\*.psd1

$moduleName = $manifestPath | Split-Path -LeafBase
$version = [version] (Get-Metadata -Path $manifestPath -PropertyName 'ModuleVersion')

"Using '$psModulePath' as base path..."
$destPath = Join-Path $psModulePath $moduleName $version

"Creating directory at '$destPath'..."
$null = New-Item -Path $destPath -ItemType 'Directory' -Force -ErrorAction 'Ignore'

"Copying items from '$ModuleRoot' to '$destPath'..."
$null = Copy-Item -Path "$ModuleRoot\*" -Destination $destPath -Recurse -Force
