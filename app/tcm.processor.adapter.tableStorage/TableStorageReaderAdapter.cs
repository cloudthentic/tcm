using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace tcm.processor.adapter.tableStorage
{
    public class TableStorageReaderAdapter
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");

        public string ReadProduct(string id)
        {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("tcmapp");

            string partitionKey = id.Split('|')[0];
            string rowKey = id.Split('|')[1];

            TableOperation operation = TableOperation.Retrieve<CapabilityEntity>(partitionKey, rowKey);

            TableResult result = table.ExecuteAsync(operation).Result;
            var capability = result.Result as CapabilityEntity;

            return capability.products;
        }
    }
}
