Install-Module AzTable
Import-Module AzTable

Add-AzAccount
$rgName = "nowyswiatfn"
$storageAccountName = "nowyswiatfn3bd064"

$account = Get-AzStorageAccount -ResourceGroupName $rgName -Name $storageAccountName
$account
$table = (Get-AzStorageTable -Context $account.Context -Name "NumberOfPatrons").CloudTable
#Get-AzTableRow -table $table -PartitionKey "partition1" | Select-Object NoOfPatrons, RowKey, TotalAmount, MonthlyAmount | Format-Table

$data = Get-AzTableRow -table $table -PartitionKey "partition1" | Select-Object RowKey, NoOfPatrons, MonthlyAmount, TotalAmount # | ConvertTo-Csv

foreach($item in $data) {
    $item.TotalAmount = $item.TotalAmount.Replace(" ", "")
    $item.MonthlyAmount = $item.MonthlyAmount.Replace(" ", "")
}

$data | ConvertTo-Csv | Out-File -FilePath "C:/temp/stats.csv"


# -----------------------------------
# Create blob storage
$blobStorageAccountKey=$(az storage account keys list -g $rgName -n $storageAccountName --query "[0].value" --output tsv)
az storage container create --name plots --account-name $storageAccountName --account-key $blobStorageAccountKey
#az storage container create --name thumbnails --account-name $blobStorageAccount --account-key $blobStorageAccountKey --public-access container