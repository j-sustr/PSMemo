
$moduleManifestPath = Convert-Path .\src\*.psd1
$module = Import-Module $moduleManifestPath -Force -PassThru -Verbose
