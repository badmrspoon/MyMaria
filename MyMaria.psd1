@{
    ModuleVersion        = '0.1.0'
    Author               = 'Phil Silva'
    GUID                 = 'c07946ac-31e8-4940-ad27-dd535b06c8af'
    Description          = 'A simple PowerShell module for working with MySQL and MariaDB'
    CompatiblePSEditions = @('Core', 'Desk')
    RootModule           = 'MyMaria.dll'
    AliasesToExport      = @()
    FunctionsToExport    = @()
    CmdletsToExport      = @(
        'Connect-Database'
        'Disconnect-Database'
        'Invoke-NonQuery'
        'Invoke-Query'
    )
    VariablesToExport    = @(
        'MyMariaConnection'
    )
    PrivateData          = @{
        PSData = @{
            Tags       = @(
                'Database'
                'MariaDB'
                'MySQL'
                'SQL'
            )
            ProjectUri = 'https://github.com/badmrspoon/MyMaria'
            LicenseUri = 'https://github.com/badmrspoon/MyMaria/blob/main/LICENSE'
            # IconUri = ''
            # ReleaseNotes = ''
        } # End of PSData hashtable
    } # End of PrivateData hashtable
}
