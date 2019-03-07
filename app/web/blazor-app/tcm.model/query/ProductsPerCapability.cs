using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.model.query
{
    /// <summary>
    /// Each capability has its lists of products and information about presence of the capability for each product
    /// </summary>
    public class ProductsPerCapability
    {
        public ProductsPerCapabilityId ProductsPerCapabilityId { get; set; }

        public Capability Capability { get; set; }

        public List<ProductHasCapability> Products { get; set; }

    }

    public class ProductsPerCapabilityId
    {
        public Guid Id { get; set; }
    }

}
