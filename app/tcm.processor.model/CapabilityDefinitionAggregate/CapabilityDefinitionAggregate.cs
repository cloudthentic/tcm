using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model.CapabilityDefinitionAggregate
{
    public class CapabilityDefinitionAggregate
    {
        public string CapabilityId { get; set; } 
        public IList<CapabilityDefinitionAttribute> CapabilityAttributes { get; private set; } 
    }
}
