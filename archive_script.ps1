$source = 'C:\Users\user\AppData\Macro Deck\plugins\Shlop.WindowsUtilsRevamped\'
$zip = Join-Path $source 'Windows Utils Revamped.zip'
$plugin = Join-Path $source 'Windows Utils Revamped.macroDeckPlugin'

# Remove old zip if exists
if (Test-Path $zip) { Remove-Item $zip -Force }

# Get all files except the plugin archive
$files = Get-ChildItem $source | Where-Object { $_.Name -ne 'Windows Utils Revamped.macroDeckPlugin' }

# Create zip archive
Compress-Archive -Path ($files.FullName) -DestinationPath $zip -Force

# Remove old plugin archive if exists
if (Test-Path $plugin) { Remove-Item $plugin -Force }

# Rename zip to macroDeckPlugin
Rename-Item -Path $zip -NewName 'Windows Utils Revamped.macroDeckPlugin'
