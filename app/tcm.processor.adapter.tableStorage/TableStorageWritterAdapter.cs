using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace tcm.processor.adapter.tableStorage
{
    /// <summary>
    /// Writes product list to Azure Table Storage
    /// </summary>
    public class TableStorageWritterAdapter
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");

        public void WriteProductAggregateListToTableStorage(IList<model.ProductAggregate> products)
        {

            // convert Productaggregates into CapabilityAggregate
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("tcmapp");

            CapabilityEntity entity = new CapabilityEntity();

            TableBatchOperation batchOperation = new TableBatchOperation();
            batchOperation.InsertOrReplace(entity);

            
            IList<TableResult> result = table.ExecuteBatchAsync(batchOperation).Result;
            //TableResult result = table.ExecuteAsync(operation).Result;
            
        }

        private IList<CapabilityEntity> ConvertProductsToTableCapabilityEntity(IList<model.ProductAggregate> products)
        {

        }
    }
}
