# Consider recreating an infrastructure not for everything if not needed
param(
    [Switch]$keepDotNet = $false,
    [Switch]$keepJS = $false,
    [string]$buildName = 'stocks'
    )

# --------------------------------------------------------------------
# Stop and remove all running containers
# We are not running multiple environments at the same time, it's fine to take all down
.\stop-infrastructure-stocks.ps1
.\components-cleaner-stocks.ps1 -keepDotNet $keepDotNet -keepJS $keepJS

# Start the infrastructure considering all the aspects of script locations
.\start-infrastructure-stocks.ps1