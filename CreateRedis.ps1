$location = "westeurope"
$resourceGroup = "az204-redis-rg"
$redisName = "az204redis" + (Get-Random).ToString() 

Add-AzAccount

az group create --name $resourceGroup --location $location
az redis create --location $location --resource-group $resourceGroup --name $redisName --sku Basic --vm-size c0