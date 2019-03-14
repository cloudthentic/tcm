using System.Collections.Generic;

namespace tcm.processor.model
{
    public class Capability
    {
        public string Id { get; set; }

        public CapabilityAttributes capabilityAttributes { get; }

        public Capability(string id)
        {
            Id = id;
            this.capabilityAttributes = new CapabilityAttributes();
        }


    }
}