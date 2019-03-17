using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace tcm.processor.adapter.tableStorage
{
    public class TableStorageReaderAdapter
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");
        TelemetryClient telemetry;

        public TableStorageReaderAdapter()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("362767d6-2f0d-48cc-b789-ae478063e59f");
            telemetry = new TelemetryClient(config);
        }

        public string ReadProduct(string capability, string attribute)
        {
            try
            {
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("tcmapp");

                string partitionKey = capability;
                string rowKey = attribute;

                TableOperation operation = TableOperation.Retrieve<CapabilityEntity>(partitionKey, rowKey);

                TableResult result = table.ExecuteAsync(operation).Result;
                telemetry.GetMetric("TableStorageReads").TrackValue(1);
                telemetry.TrackEvent("TableStorageReaderAdapter.ReadProduct Invoked");

                var capabilityEntity = result.Result as CapabilityEntity;
                return capabilityEntity.products;
            }
            catch(Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
            
        }
    }
}
