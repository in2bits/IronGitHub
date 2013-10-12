@echo off

# Usage CreateNuGetPackage.cmd 1.2.0

set framework=v4.0.30319


set configuration=Release

rmdir /s /d bin\%configuration%

"%SystemDrive%\Windows\Microsoft.NET\Framework\%framework%\MSBuild.exe" IronGitHub.sln /p:Configuration=%configuration% /p:Platform="Any CPU" /p:OutputPath=bin\%configuration%\net40
"%SystemDrive%\Windows\Microsoft.NET\Framework\%framework%\MSBuild.exe" IronGitHub.sln /p:Configuration=%configuration% /p:Platform="Any CPU" /p:TargetFrameworkVersion=4.5 /p:OutputPath=bin\%configuration%\net45

.nuget\NuGet.exe pack IronGitHub.nuspec -BasePath IronGitHub\bin\%configuration% -Version %1

pause