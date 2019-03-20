using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model
{
    /// <summary>
    /// All products with specific capability and associated values for capability attributes 
    /// </summary>
    /// <example>
    /// capability: Network|0.1
    /// attribute: Service-Endpoint
    /// products: [   {     "product": "CosmosDB",     "hasCapability": "Yes"   },   {     "product": "App Service",     "hasCapability": "Yes"   },   {     "product": "API Management",     "hasCapability": "No"   },   {     "product": "IoT Hub",     "hasCapability": "No--"   } ]
    /// </example>
    public class CapabilityAggregate
    {
        public string CapabilityId { get; set; }
        public string Attribute { get; set; }

        public IList<ProductHasCapabilityAttribute> ListOfProductHasCapabilityAttribute { get; private set; }

        public void AddNewProductCapabilityAttributeToTheList(string product, string hasCapability, string productId)
        {
            string[] items = productId.Split('|');
            string productVersion = String.Empty;
            string productName;

            if (items.Length == 2)
            {
                productVersion = items[1];
                productName = ($"{product}|{productVersion}");
            }
            else productName = product;

            if (ListOfProductHasCapabilityAttribute == null) this.ListOfProductHasCapabilityAttribute = new List<ProductHasCapabilityAttribute>();
            this.ListOfProductHasCapabilityAttribute.Add(new ProductHasCapabilityAttribute(productName, hasCapability));
        }

    }
}
