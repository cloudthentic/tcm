using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using tcm.processor;

namespace tcm.processor.adapter.tableStorage
{
    public class TableStorageReaderAdapter : TableStorageAdapterBase
    {

        public TableStorageReaderAdapter(string connection) : base(connection) { }
        

        public string ReadProduct(string capability, string attribute)
        {
            try
            {
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(this.tableName);

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

        public CapabilityEntity ReadProductEntities(string capability, string attribute)
        {
            try
            {
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(this.tableName);

                string partitionKey = capability;
                string rowKey = attribute;

                TableOperation operation = TableOperation.Retrieve<CapabilityEntity>(partitionKey, rowKey);

                TableResult result = table.ExecuteAsync(operation).Result;
                telemetry.GetMetric("TableStorageReads").TrackValue(1);
                telemetry.TrackEvent("TableStorageReaderAdapter.ReadProduct Invoked");

                var capabilityEntity = result.Result as CapabilityEntity;
                return capabilityEntity;
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
        }

        public string ReadCapabilities(string id)
        {
            try
            {
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(this.tableName);

                TableOperation operation = TableOperation.Retrieve<CapabilityEntity>("capability", id);

                TableResult result = table.ExecuteAsync(operation).Result;
                var capability = result.Result as CapabilityEntity;

                return capability.products;
            }
            catch (Exception ex)
            {
                telemetry.TrackException(ex);
                throw ex;
            }
        }

        public IList<model.CapabilityView> ReadCapabilityView()
        {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(this.tableName);

            TableQuery<CapabilityEntity> query = new TableQuery<CapabilityEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "capability"));

            TableContinuationToken token = new TableContinuationToken();
            var result = table.ExecuteQuerySegmentedAsync(query, token).Result;
            var res = result.Results as IList<CapabilityEntity>;

            List<model.CapabilityView> viewList = new List<model.CapabilityView>();
            foreach(var item in res)
            {
                var record = new model.CapabilityView(item.RowKey, item.RowKey);
                viewList.Add(record);
            }

            return viewList;
        }

    }
}
