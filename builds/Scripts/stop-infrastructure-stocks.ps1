param(
	[string]$buildName = 'stocks'
)

# Only stop the infrastructure
cd ..
docker compose -p $buildName -f docker-compose-stocks.yml down

# Get back to the foldere we were in
cd .\Scripts\