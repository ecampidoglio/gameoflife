function Out-LogFile {
<#
.Synopsis
    Log the specified message to a file.
.Description
    Helper function to output the contents of a string to a log file.
.Parameter Path
    The path to the file where to log the message.
.Parameter Message
    The string consisting of the message to log.
#>
[CmdletBinding()]
param(
    [Parameter(Position=1)]
    [string]$Path,
    [Parameter(Position=2, ValueFromPipeline=$true)]
    [string]$Message
)
    if ($Message.Length -gt 0) {
        $now = Get-Date
        Write-Output "$now - $message" | Out-File $Path -Append
    }
}

function Get-CertPrivateKey {
<#
.Synopsis
    Gets the private key file associated to the specified X.509 certificate.
.Description
    Helper function to retrieve the private key file on disk associated to a X.509 certificate.
.Parameter Store
    The path to certificate store where to certificate is located.
.Parameter SubjectName
    The fully-qualified subject name of the certificate.
#>
[CmdletBinding()]
param(
    [Parameter(Position=1)]
    [string]$Store,
    [Parameter(Position=2)]
    [string]$SubjectName
)
    $certificate = Get-ChildItem Cert:\$Store | Where-Object { $_.Subject -match $SubjectName }
    $privateKeyName = $certificate.PrivateKey.CspKeyContainerInfo.UniqueKeyContainerName
    $rootStore = Split-Path -Parent $Store

    if ($rootStore -eq "LocalMachine") {
        Get-ChildItem "$Env:ProgramData\Microsoft\Crypto\RSA\MachineKeys" -Recurse | Where-Object { $_.Name -eq $privateKeyName }
    }
    elseif ($rootStore -eq "CurrentUser") {
        Get-ChildItem "$Env:AppData\Microsoft\Crypto\RSA" -Recurse | Where-Object { $_.Name -eq $privateKeyName }
    }
}

function Import-PSSnapin {
<#
.Synopsis
    Adds the Windows PowerShell snap-in with the specified name to the current session.
    This cmdlet will not throw an exception if the specified snap-in is already present in the session.
.Description
    Helper function to safely add the Windows PowerShell snap-in with the specified name to the current session.
.Parameter Name
    The name of the snap-in to import.
#>
[CmdletBinding()]
param(
    [Parameter(Position=1)]
    [string]$Name
)
    $snapinIsNotInSession = -Not (Get-PSSnapin $Name -ErrorAction SilentlyContinue)

    if ($snapinIsNotInSession) {
        Add-PSSnapin $Name
    }
}

function Get-CachedCredentials {
<#
.Synopsis
    Gets a credential object based on a user name and password stored in an XML file.
.Description
    Helper function to retrieve a credential object based on the contents of an XML file.
    If the credentials file is not present at the specified location, the user will be prompted to enter a user name and password.
.Parameter Path
    The path to the XML file to import the credentials from.
#>
[CmdletBinding()]
param(
    [Parameter(Position=1)]
    [string]$Path = "Credentials.xml"
)
    if (Test-Path $Path) {
        $cache = Import-Clixml $Path
        $key = [System.Convert]::FromBase64String($cache.Secret)
        $password = $cache.EncryptedPassword | ConvertTo-SecureString -Key $key
        New-Object System.Management.Automation.PSCredential $cache.UserName, $password
    } else {
        Get-Credential
    }
}

function Set-CachedCredentials {
<#
.Synopsis
    Stores a pair of credentials in an XML file based on a user name and password.
.Description
    Helper function to securely store a pair of credentials in an XML file.
.Parameter Path
    The path to the XML file to export the credentials to.
#>
[CmdletBinding()]
param(
    [Parameter(Position=1)]
    [string]$Path = "Credentials.xml"
)
    $key = New-StrongPasswordBytes -Length 32
    $textualKey = [System.Convert]::ToBase64String($key)
    $credentials = Get-Credential
    $securePassword = $credentials.Password | ConvertFrom-SecureString -Key $key
    $cache = New-Object PSObject -Property @{ "UserName" = $credentials.UserName; "EncryptedPassword" = $securePassword; "Secret" = $textualKey }
    $cache.PSObject.TypeNames.Insert(0, "CachedPSCredential")
    $cache | Export-Clixml $Path
}

function New-StrongPasswordBytes ($length) {
    Add-Type -Assembly System.Web
    $password = [System.Web.Security.Membership]::GeneratePassword($length, $length / 2)
    [System.Text.Encoding]::UTF8.GetBytes($password)
}
