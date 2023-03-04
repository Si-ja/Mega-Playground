param(
    $keepDotNet = $false,
    $keepJS = $false
    )

# ---------------------------------------------------------------------------

Write-Output 'Y' | docker volume prune
Write-Output 'Y' | docker network prune

$DeletionImagesDotNet = (
    'stocks-stocksapi',
    'stocks-marketapi'
)

$DeletionImagesJS = (
    'stocks-userinterface'
)

# And remove the image we no longer need if so the user chooses
if (-Not $keepDotNet)
{
    foreach($DockerImage in $DeletionImagesDotNet)
    {
        docker rmi $DockerImage -f
    }
}

if(-Not $keepJS)
{
    foreach($DockerImage in $DeletionImagesJS)
    {
        docker rmi $DockerImage -f
    }
}

# And prune whatever we have for the sake of this computer I still want to keep alive
# But only dangling components
Write-Output 'Y' | docker system prune