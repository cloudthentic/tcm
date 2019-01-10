using System;
using System.Runtime.Serialization;

namespace tcm.model.query
{

    /// <summary>
    /// Data Class which is used in a query response to list status of capability for each product.
    /// 
    /// </summary>
    public class ProductHasCapability
    {

        public string ProductName { get; set; }

        public ProductId ProductId { get; set; }

        public bool HasCapability { get; set; }
    }
}
