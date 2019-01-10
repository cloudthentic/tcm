using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.model
{
    public class CapabilityId
    {
        public Guid Id { get; set; }
    }

    public class Capability
    {
        public CapabilityId Id { get; set; }
        public string Name { get; set; }
    }
}
