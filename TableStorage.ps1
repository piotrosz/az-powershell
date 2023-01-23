Install-Module AzTable
Import-Module AzTable

Add-AzAccount

$account = Get-AzStorageAccount -ResourceGroupName "PiotrResourceGroup" -Name "nowyswiat"
$account
$table = (Get-AzStorageTable -Context $account.Context -Name "NumberOfPatrons").CloudTable
Get-AzTableRow -table $table -PartitionKey "partition1" | Select-Object NoOfPatrons, RowKey, TotalAmount, MonthlyAmount | Format-Table