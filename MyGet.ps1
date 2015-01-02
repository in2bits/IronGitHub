$username = Get-Item Env:Username
$password = Get-Item Env:Password
$repository = Get-Item Env:Repository
$version = Get-Item Env:PackageVersion

(gc .\IronGitHub.Tests\app.config).replace('key="Username" value=""','key="Username" value="' + $username + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Password" value=""','key="Password" value="' + $password + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Repository" value=""','key="Repository" value="' + $repository + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Version" value=""','key="Version" value="' + $version + '"') | sc .\IronGitHub.Tests\app.config

(gc .\IronGitHub\IronGitHub.Tests\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyVersion("1.0.0.0")]','[assembly: AssemblyVersion("' + $version + '")]') | sc .\IronGitHub\IronGitHub.Tests\Properties\AssemblyInfo.cs
(gc .\IronGitHub\IronGitHub.Tests\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyFileVersion("1.0.0.0")]','[assembly: AssemblyFileVersion("' + $version + '")]') | sc .\IronGitHub\IronGitHub.Tests\Properties\AssemblyInfo.cs
(gc .\IronGitHub\IronGitHub\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyVersion("1.0.0.0")]','[assembly: AssemblyVersion("' + $version + '")]') | sc .\IronGitHub\IronGitHub\Properties\AssemblyInfo.cs
(gc .\IronGitHub\IronGitHub\Properties\AssemblyInfo.cs).replace('[assembly: AssemblyFileVersion("1.0.0.0")]','[assembly: AssemblyFileVersion("' + $version + '")]') | sc .\IronGitHub\IronGitHub\Properties\AssemblyInfo.cs