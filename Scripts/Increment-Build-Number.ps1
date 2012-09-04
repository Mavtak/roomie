Param (
    [string]$filename = $(throw "filename is required")
)

[regex]$version_regex = "[0-9]+[.][0-9]+[.][0-9]+[.][0-9]+";

Write-Host ("Parsing " + $filename)
$matches = $version_regex.Matches((Get-Content $filename))

Write-Host ("" + $matches.Count + " matches");

if($matches.Count -eq 0)
{
    Write-Host "Error";
    Exit 1;
}

Write-Host ("Parsed value: " + $matches[0].value);
$oldVersion = New-Object -TypeName System.Version -ArgumentList $matches[0].value;
write-host ("Old Version: " + $oldVersion.ToString())


$newVersion = New-Object -TypeName System.Version -ArgumentList ("" + $oldVersion.Major + "." + $oldVersion.Minor + "." + ($oldVersion.Build + 1) + "." + $oldVersion.Revision);
write-host "New Version: " $newVersion

Write-Host ("Updating file " + $filename)
(Get-Content $filename) -replace $version_regex, $newVersion.ToString() | Set-Content $filename