using System;
using Xunit;
using tcm.processor.model;

namespace tcm.processor.adapter.tableStorage.utest
{
    public class TableStorageWritterAdapterTest
    {
        [Fact]
        public void ConvertProductsToTableCapabilityEntityTest()
        {
            ProductAggregate pa1 = new ProductAggregate();
            pa1.product = "Azure Cosmos DB";
            pa1.productId = "azure-cosmos-db|basic";
            var item = pa1.productCapabilities.AddNewCapability("Network|0.1");
            item.capabilityAttributes.AddNewAttribute("Virtual-Network-Integration", "No");
            item.capabilityAttributes.AddNewAttribute("Service-Endpoint", "No");

            ProductAggregate pa2 = new ProductAggregate();
            pa2.product = "Azure API Management";
            pa2.productId = "azure-api-management|basic";
            var item2 = pa2.productCapabilities.AddNewCapability("Network|0.1");
            item2.capabilityAttributes.AddNewAttribute("Virtual-Network-Integration", "Yes");
            item2.capabilityAttributes.AddNewAttribute("Service-Endpoint", "No");

            var list = new System.Collections.Generic.List<ProductAggregate>();
            list.Add(pa1);
            list.Add(pa2);

            adapter.tableStorage.TableStorageWritterAdapter wa = new TableStorageWritterAdapter();
            var result = wa.ConvertProductsToTableCapabilityEntity(list);
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }
    }
}
