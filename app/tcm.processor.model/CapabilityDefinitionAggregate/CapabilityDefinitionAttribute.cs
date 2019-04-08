using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model.CapabilityDefinitionAggregate
{
    public class CapabilityDefinitionAttribute
    {
        public string Name { get; set; }
        public string Id { get; set; } // [    {     "name": "Service Endpoint",     "id": "Service-Endpoint"   },   {     "name": "VNet Integration",     "id": "Virtual-Network-Integration"   } ]
    }
}
