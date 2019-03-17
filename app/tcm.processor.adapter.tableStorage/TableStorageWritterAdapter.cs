using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;

namespace tcm.processor.adapter.tableStorage
{
    /// <summary>
    /// Writes product list to Azure Table Storage
    /// </summary>
    public class TableStorageWritterAdapter
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");
        TelemetryClient telemetry; 

        public TableStorageWritterAdapter()
        {
            TelemetryConfiguration config = new TelemetryConfiguration("362767d6-2f0d-48cc-b789-ae478063e59f");
            telemetry = new TelemetryClient(config);
        }

        public void WriteProductAggregateListToTableStorage(IList<model.ProductAggregate> products)
        {
            var success = false;
            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            try
            { 
                // Application Insights correlation
                using (var telemetryOperation = telemetry.StartOperation<RequestTelemetry>("WriteProductAggregateListToTableStorage"))
                {


                    // convert Productaggregates into CapabilityAggregate
                    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                    CloudTable table = tableClient.GetTableReference("tcmapp");

                    TableBatchOperation batchOperation = new TableBatchOperation();
                    foreach (var item in this.ConvertProductsToTableCapabilityEntity(products))
                    {
                        batchOperation.InsertOrReplace(item);
                    }

                    IList<TableResult> result = table.ExecuteBatchAsync(batchOperation).Result;
                    success = true;
                    telemetry.GetMetric("TableStorageWrites").TrackValue(1);
                    telemetry.TrackEvent("TableStorageWritterAdapter.WriteProductAggregateListToTableStorage Invoked");
                }
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
            }
            finally
            {
                timer.Stop();
                telemetry.TrackDependency("Azure Table Storage", "WriteProductAggregateListToTableStorage", startTime, timer.Elapsed, success);
                telemetry.Flush();
            }

        }

        public IList<CapabilityEntity> ConvertProductsToTableCapabilityEntity(IList<model.ProductAggregate> products)
        {
            model.Services.ProductToCapabilityAggregateService service = new model.Services.ProductToCapabilityAggregateService();
            var capabilityAggregateList = service.ConvertProductToCapabilityAggregate(products);

            var capabilityEntityList = new List<CapabilityEntity>();

            foreach(var item in capabilityAggregateList)
            {
                var newEntity = new CapabilityEntity(item.CapabilityId, item.Attribute);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(item.ListOfProductHasCapabilityAttribute);
                newEntity.products = output;
                capabilityEntityList.Add(newEntity);
            }

            return capabilityEntityList;
        }
    }
}
