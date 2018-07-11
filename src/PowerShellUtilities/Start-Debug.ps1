
Import-Module .\PowerShellUtilities -Verbose
#Import-Module Newtonsoft.Json -Verbose


#$Jurisdictions = Get-JurisdictionList | Get-Jurisdiction -Verbose

#Import-Jurisdiction -Jurisdiction @($Jurisdictions)



Get-WUUpdate -Force -Verbose

