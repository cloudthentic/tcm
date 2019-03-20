using System;
using System.Collections.Generic;
using System.Text;

namespace tcm.processor.model.Services
{
    public class ProductToCapabilityAggregateService
    {
        

        public IList<CapabilityAggregate> ConvertProductToCapabilityAggregate(IList<ProductAggregate> products)
        {
            IList<CapabilityAggregate> capabilityAggregates = new List<CapabilityAggregate>();


            foreach(var product in products)
            {
                foreach(var capability in product.productCapabilities.capabilities)
                {
                    foreach(var attribute in capability.capabilityAttributes.attributes)
                    {
                        var capabilityAggregate = this.IsCapabilityAttributeCreated(capabilityAggregates, capability.Id, attribute.Name);
                        if(capabilityAggregate == null) this.AddNewCapabilityAndProductIntoCapabilityAttributeList(capabilityAggregates, capability, product, attribute);
                        else capabilityAggregate.AddNewProductCapabilityAttributeToTheList(product.product, attribute.Value, product.productId);
                    }
                }
            }


            return capabilityAggregates;
        }

        private CapabilityAggregate IsCapabilityAttributeCreated(IList<CapabilityAggregate> capabilityAggregates, string capabilityId, string attributeName)
        {
            foreach(var capabilityAggregate in capabilityAggregates)
            {
                if (capabilityAggregate.CapabilityId == capabilityId && capabilityAggregate.Attribute == attributeName) return capabilityAggregate;
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="capabilityAggregates"></param>
        /// <param name="capability"></param>
        /// <param name="product"></param>
        private void AddNewCapabilityAndProductIntoCapabilityAttributeList(
            IList<CapabilityAggregate> capabilityAggregates, 
            Capability capability, 
            ProductAggregate product,
            CapabilityAttribute  attribute)
        {
            CapabilityAggregate ca = new CapabilityAggregate();
            ca.CapabilityId = capability.Id;
            ca.Attribute = attribute.Name;
            ca.AddNewProductCapabilityAttributeToTheList(product.product, attribute.Value, product.productId);
            capabilityAggregates.Add(ca);

        }
    }
}
