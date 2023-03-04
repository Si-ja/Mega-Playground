param(
	[string]$buildName = 'stocks'
)

# This will give us a reference where we can call docker-compose from
# $PSScriptRoot
cd ..

# Bring up the docker infrastructure
docker compose -p $buildName -f docker-compose-stocks.yml up -d

# Invoke creation of Hangfire jobs if they don't exist yet
# For this example Hangfire should accept such calls as requests are set to be accepted from the .NET side
# But overal in the real world this might end up you ending on a naughty list with Santa
# Also adding a small delay to wait for services to fully start everywhere
ECHO "Waiting for 5 seconds..."
Start-Sleep -Seconds 5

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls -bor [Net.SecurityProtocolType]::Tls11 -bor [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -URI http://localhost:8002/CreateJobs

# Get back to where the user was before
cd .\Scripts\