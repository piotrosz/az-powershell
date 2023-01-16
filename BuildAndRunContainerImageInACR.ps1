$vsSubscription = Get-AzSubscription | Where-Object Name -Like "Visual Studio Professional*"
$vsSubscription

Set-AzContext -Subscription $vsSubscription.SubscriptionId
Get-AzContext

$resourceGroupName = "az204-acr-rg"

New-AzResourceGroup -ResourceGroupName $resourceGroupName -Location "westeurope"

Get-AzResourceGroup | Select-Object ResourceGroupName

$containerRegistryName = "piotrcontainerregistry"

Test-AzContainerRegistryNameAvailability -Name $containerRegistryName

New-AzContainerRegistry -ResourceGroupName $resourceGroupName -Name $containerRegistryName -Sku Basic

# I don't know how to do it in Az PowerShell
az acr build --image sample/hello-world:v1  --registry $containerRegistryName --file Dockerfile .

Connect-AzContainerRegistry -Name $containerRegistryName

Get-AzContainerRegistryRepository -RegistryName $containerRegistryName

Get-AzContainerRegistryTag -RegistryName $containerRegistryName -RepositoryName "sample/hello-world"

az acr run --registry $containerRegistryName --cmd '$Registry/sample/hello-world:v1' /dev/null

Remove-AzResourceGroup -Name $resourceGroupName