$result = Start-Process "msiexec.exe" -ArgumentList "/i C:\!Personal\PrivateRepo\AzureFunctions\AzureStorage.Tests\bin\Debug\net7.0\Resources\v1.0.61\NaturalnieAppInstaller.msi /q" -Wait -NoNewWindow 2>&1 | Out-String

Write-Output "Test"
Write-Output $result.ToString()