Add-AzAccount

$resourceGroup = "az204-eh-rg"
$namespaceName = "piotrnamespace-eh-ns"
$eventHubName = "piotreventhub-eh-hub"

New-AzResourceGroup -Name $resourceGroup -Location westeurope
New-AzEventHubNamespace -ResourceGroupName $resourceGroup -NamespaceName $namespaceName -Location westeurope
New-AzEventHub -ResourceGroupName $resourceGroup -NamespaceName $namespaceName -EventHubName $eventHubName -MessageRetentionInDays 3