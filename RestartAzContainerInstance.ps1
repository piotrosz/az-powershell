Install-Module -Name Az -Scope CurrentUser -Repository PSGallery -Force

# Login to Azure
Connect-AzAccount

# Set proper subscription
Get-AzSubscription

$subscriptionId = "Fill in subscription is "
Set-AzContext -Subscription $subscriptionId

# Get resource groups
Get-AzResourceGroup | Select-Object ResourceGroupName
$resourceGroupName = "Fill in resource group name"

# Get resource
Get-AzResource -ResourceGroupName $resourceGroupName

Stop-AzContainerGroup -SubscriptionId $subscriptionId -Name "" -ResourceGroupName $resourceGroupName
#Start-AzContainerGroup
#Restart-AzContainerGroup