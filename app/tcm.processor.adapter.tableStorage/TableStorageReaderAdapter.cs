﻿using System;
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

        public CapabilityEntity ReadProductEntities(string capability, string attribute)
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
                CloudTable table = tableClient.GetTableReference("tcmapp");

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

    }
}
