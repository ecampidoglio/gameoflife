Include "Utilities.ps1"

Properties {
    $ComputerName = "localhost"
    $Credentials = $(Get-CachedCredentials)
    $AppPoolName = $WebSiteName
    $Port = 80
    $PackageParameters = @{}
    $PublishSettingsFilePath = "$WebSiteName.publishSettings"
}

Task default -Depends Install
Task Install -Depends ValidateParameters, CreateWebSite, DeployApplication
Task Update -Depends ValidateParameters, DeployApplication

Task ValidateParameters {
    Assert($WebSiteName -ne $null) "The 'WebSiteName' parameter is required."
    Assert($PhysicalPath -ne $null) "The 'PhysicalPath' parameter is required."
    Assert($PackagePath -ne $null) "The 'PackagePath' parameter is required."
}

Task DeployApplication -Depends CreatePublishSettings {
    $parameters = @{ "IIS Web Application Name" = $WebSiteName }
    $parameters += $PackageParameters

    Restore-WDPackage `
        -Package $PackagePath `
        -Parameters $parameters `
        -DestinationPublishSettings $PublishSettingsFilePath | Out-Null
}

Task CreatePublishSettings {
    New-WDPublishSettings `
        -ComputerName $ComputerName `
        -Site $WebSiteName `
        -Credentials $Credentials `
        -EncryptPassword `
        -AllowUntrusted `
        -AgentType MSDepSvc `
        -FileName $PublishSettingsFilePath | Out-Null
}

Task CreateWebSite -Depends DeleteWebSite {
    Invoke-Command `
        -ComputerName $ComputerName `
        -Credential $Credentials `
        -ScriptBlock {
            Import-Module WebAdministration

            if (-Not (Test-Path $using:PhysicalPath)) {
                mkdir $using:PhysicalPath | Out-Null
            }

            New-WebAppPool -Name $using:AppPoolName | Out-Null

            $appPoolPath = "IIS:\AppPools\$using:AppPoolName"
            Set-ItemProperty $appPoolPath -Name managedRuntimeVersion -Value v4.0
            Set-ItemProperty $appPoolPath -Name enable32BitAppOnWin64 -Value $true

            $sites = ls IIS:\\Sites
            $maxId =  $sites.Id | sort -Descending | select -First 1
            New-Website -Id ($maxId + 1) `
                        -Name $using:WebSiteName `
                        -Port $using:Port `
                        -PhysicalPath $using:PhysicalPath `
                        -ApplicationPool $using:AppPoolName | Out-Null
        }
}

Task DeleteWebSite {
    Invoke-Command `
        -ComputerName $ComputerName `
        -Credential $Credentials `
        -ScriptBlock {
            Import-Module WebAdministration

            if (Test-Path "IIS:\AppPools\$using:AppPoolName") {
                Remove-WebAppPool -Name $using:AppPoolName
            }

            if (Test-Path "IIS:\Sites\$using:WebSiteName") {
                Remove-WebSite -Name $using:WebSiteName
            }
        }
}
