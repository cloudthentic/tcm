using System;
using System.Collections.Generic;

namespace tcm.processor.model
{
    public class ProductCapabilities
    {

        public System.Collections.Generic.IList<Capability> capabilities { get; }

        public ProductCapabilities()
        {
            capabilities = new List<Capability>();
        }

        public void ParseIntoCapability(System.Collections.Generic.IList<ProductAggregate> listOfProductAggregates)
        {

        }


        public Capability AddNewCapability(string id)
        {
            Capability capability = new Capability(id);
            capabilities.Add(capability);
            return capability;
        }
    }
}
