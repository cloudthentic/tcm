using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model
{
    public class ProductHasCapabilityAttribute
    {
        public string Product { get; set; }
        public string HasCapability { get; set; }

        public ProductHasCapabilityAttribute(string product, string hasCapability)
        {
            this.Product = product;
            this.HasCapability = hasCapability;
        }
    }
}
