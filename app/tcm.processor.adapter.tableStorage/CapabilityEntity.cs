using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace tcm.processor.adapter.tableStorage
{
    /// <summary>
    /// generic Table entity used to persist both CapabilityAggregate and CapabilityDefinitionAggregate
    /// </summary>
    public class CapabilityEntity : TableEntity
    {
        public CapabilityEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public CapabilityEntity() { }

        public string products { get; set; }

    }
}
