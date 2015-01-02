$username = Get-Item Env:Username
$password = Get-Item Env:Password
$repository = Get-Item Env:Repository

(gc .\IronGitHub.Tests\app.config).replace('key="Username" value=""','key="Username" value="' + $username + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Password" value=""','key="Password" value="' + $password + '"') | sc .\IronGitHub.Tests\app.config
(gc .\IronGitHub.Tests\app.config).replace('key="Repository" value=""','key="Repository" value="' + $repository + '"') | sc .\IronGitHub.Tests\app.config
