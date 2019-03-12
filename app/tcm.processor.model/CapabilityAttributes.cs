using System.Collections.Generic;

namespace tcm.processor.model
{
    public class CapabilityAttributes
    {
        public System.Collections.Generic.IList<CapabilityAttribute> attributes { get;  }

        public CapabilityAttributes()
        {
            this.attributes = new List<CapabilityAttribute>();
        }
        public CapabilityAttribute AddNewAttribute(string name, string value)
        {
            var attribute = new CapabilityAttribute(name, value);
            this.attributes.Add(attribute);
            return attribute;
        }
    }
}