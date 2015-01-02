$username = Get-Item Env:Username
$password = Get-Item Env:Password
$repository = Get-Item Env:Repository
$version = Get-Item Env:PackageVersion

$msBuild = @'
msbuild .\IronGitHub.sln
'@

echo @'
##myget[message text='FAKE' errorDetails='stack trace' status='ERROR']
@'

$msTest = @'
mstest /testcontainer:'.\IronGitHub.Tests\bin\Debug\IronGitHub.Tests.dll'
'@

(gc .\IronGitHub.Tests\app.config).replace('key="Username" value=""','key="Username" value="' + $username + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Password" value=""','key="Password" value="' + $password + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Repository" value=""','key="Repository" value="' + $repository + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Version" value=""','key="Version" value="' + $version + '"') | sc .\IronGitHub.Tests\app.config

(gc .\IronGitHub.Tests\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyVersion("1.0.0.0")]','[assembly: AssemblyVersion("' + $version + '")]') | sc .\IronGitHub.Tests\Properties\AssemblyInfo.cs
(gc .\IronGitHub.Tests\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyFileVersion("1.0.0.0")]','[assembly: AssemblyFileVersion("' + $version + '")]') | sc .\IronGitHub.Tests\Properties\AssemblyInfo.cs
(gc .\IronGitHub\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyVersion("1.0.0.0")]','[assembly: AssemblyVersion("' + $version + '")]') | sc .\IronGitHub\Properties\AssemblyInfo.cs
(gc .\IronGitHub\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyFileVersion("1.0.0.0")]','[assembly: AssemblyFileVersion("' + $version + '")]') | sc .\IronGitHub\Properties\AssemblyInfo.cs

Invoke-Expression -Command:$msBuild