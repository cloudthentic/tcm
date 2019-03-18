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
        CloudStorageAccount storageAccount;
        TelemetryClient telemetry;

        public TableStorageWritterAdapter(string connection)
        {
            storageAccount = CloudStorageAccount.Parse(connection);
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

                // convert Productaggregates into CapabilityAggregate
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference("tcmapp");

                var items = this.ConvertProductsToTableCapabilityEntity(products);
                
                var partitions = this.GetPartitionKeyList(items);

                // insert or replace in batch and split batch per partition key
                foreach(var partition in partitions)
                {
                    TableBatchOperation batchOperation = new TableBatchOperation();
                    foreach(var item in items)
                    {
                        // add only if the item belongs to the partition
                        if(item.PartitionKey == partition) batchOperation.InsertOrReplace(item);
                    }
                    IList<TableResult> result = table.ExecuteBatchAsync(batchOperation).Result;

                }

                success = true;
                telemetry.GetMetric("TableStorageWrites").TrackValue(1);
                telemetry.TrackEvent("TableStorageWritterAdapter.WriteProductAggregateListToTableStorage Invoked");
                
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
            finally
            {
                timer.Stop();
                telemetry.TrackDependency("Azure Table Storage", "WriteProductAggregateListToTableStorage", startTime, timer.Elapsed, success);
                telemetry.Flush();
            }

        }

        public IList<string> GetPartitionKeyList(IList<CapabilityEntity> entities)
        {
            var keys = new List<string>();
            foreach(var item in entities)
            {
                if (keys.BinarySearch(item.PartitionKey) < 0) keys.Add(item.PartitionKey);
            }
            return keys;
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
