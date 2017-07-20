$devPromptFolder = Get-ChildItem -Path "C:\Program Files (x86)\Microsoft Visual Studio" -Filter VsDevCmd.bat -Recurse -ErrorAction SilentlyContinue -Force | % { $_.Directory.FullName }
pushd $devPromptFolder
cmd /c "VsDevCmd.bat&set" |
foreach {
  if ($_ -match "=") {
    $v = $_.split("="); set-item -force -path "ENV:\$($v[0])"  -value "$($v[1])"
  }
}
popd
Write-Host "`nVisual Studio 2017 Command Prompt variables set." -ForegroundColor Yellow
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
Push-Location $dir

if(-Not(Test-Path nuget.exe))
{
    wget https://dist.nuget.org/win-x86-commandline/v4.1.0/nuget.exe -OutFile nuget.exe
}

.\nuget restore ..\src\PCFDotNetLegacyToCloudNative.sln

msbuild /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true /p:PublishUrl=..\..\publish\FortunesFormsUI  /p:OutputPath=tmp  ..\src\FortunesFormsUI\FortunesFormsUI.csproj
Remove-Item ..\src\FortunesFormsUI\tmp -recurse
msbuild /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true /p:PublishUrl=..\..\publish\FortunesLegacyService  /p:OutputPath=tmp  ..\src\FortunesLegacyService\FortunesLegacyService.csproj
Remove-Item ..\src\FortunesLegacyService\tmp -recurse
msbuild /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true /p:PublishUrl=..\..\publish\FortunesServicesOwin  /p:OutputPath=tmp  ..\src\FortunesServicesOwin\FortunesServicesOwin.csproj
Remove-Item ..\src\FortunesServicesOwin\tmp -recurse
msbuild /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true /p:PublishUrl=..\..\publish\FortunesUI  /p:OutputPath=tmp  ..\src\FortunesUI\FortunesUI.csproj
Remove-Item ..\src\FortunesUI\tmp -recurse

Copy-Item manifest.yml ..\publish\manifest.yml