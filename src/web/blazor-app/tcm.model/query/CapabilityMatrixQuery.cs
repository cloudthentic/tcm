using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.model.query
{
    public class CapabilityMatrixQuery
    {

        private List<Product> products;

        public List<ProductHasCapability> ViewProductListFor(Capability capability)
        {
            // find in the repository ProductPerCapability list for specified capability
            return new List<ProductHasCapability>();
        }
    }
}

