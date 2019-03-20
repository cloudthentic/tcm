using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model
{

    public class CapabilityView
    {
        public CapabilityView(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
