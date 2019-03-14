using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace tcm.processor.adapter.tableStorage
{
    public class CapabilityEntity : TableEntity
    {
        public CapabilityEntity(string capability, string capabilityAttribute)
        {
            this.PartitionKey = capability;
            this.RowKey = capabilityAttribute;
        }

        public CapabilityEntity() { }

        public string products { get; set; }

    }
}
