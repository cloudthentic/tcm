using System;
using Xunit;

namespace tcm.processor.model.utest
{
    public class ProductToCapabilityAggregateServiceTest
    {
        [Fact]
        public void ConvertProductToCapabilityAggregateTest()
        {
            Services.ProductToCapabilityAggregateService service = new Services.ProductToCapabilityAggregateService();

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

            var result = service.ConvertProductToCapabilityAggregate(list);
            Assert.NotNull(result);

            Assert.True(result[0].CapabilityId == "Network|0.1");
            Assert.True(result[0].Attribute == "Virtual-Network-Integration");
            Assert.True(result[0].ListOfProductHasCapabilityAttribute[0].Product == "Azure Cosmos DB|basic");
            Assert.True(result[0].ListOfProductHasCapabilityAttribute[0].HasCapability == "No");
            Assert.True(result[0].ListOfProductHasCapabilityAttribute[1].Product == "Azure API Management|basic");
            Assert.True(result[0].ListOfProductHasCapabilityAttribute[1].HasCapability == "Yes");

            Assert.True(result[1].CapabilityId == "Network|0.1");
            Assert.True(result[1].Attribute == "Service-Endpoint");
            Assert.True(result[1].ListOfProductHasCapabilityAttribute[0].Product == "Azure Cosmos DB|basic");
            Assert.True(result[1].ListOfProductHasCapabilityAttribute[0].HasCapability == "No");
            Assert.True(result[1].ListOfProductHasCapabilityAttribute[1].Product == "Azure API Management|basic");
            Assert.True(result[1].ListOfProductHasCapabilityAttribute[1].HasCapability == "No");
        }
    }
}
