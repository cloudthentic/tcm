using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.model
{

    public class Capability
    {
        public Capability() { }

        public Capability(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
