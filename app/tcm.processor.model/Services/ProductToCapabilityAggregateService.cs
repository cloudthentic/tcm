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
                this.MapIntoNewOrExistingCapabilityAggregate(capabilityAggregates, product);
            }

            return capabilityAggregates;
        }

        private void MapIntoNewOrExistingCapabilityAggregate(
            IList<CapabilityAggregate> capabilityAggregates, 
            ProductAggregate product)
        {
            foreach(Capability capability in product.productCapabilities.capabilities)
            {
                this.MergeCapabilityInto(capabilityAggregates, capability, product);
            }
        }

        private void MergeCapabilityInto(
            IList<CapabilityAggregate> capabilityAggregates, 
            Capability capability, 
            ProductAggregate product)
        {
            foreach(CapabilityAggregate capabilityAggregate in capabilityAggregates)
            {
                foreach(var item in capability.capabilityAttributes.attributes)
                {
                    if (capabilityAggregate.CapabilityId == capability.Id && capabilityAggregate.Attribute == item.Name)
                    {
                        this.AddNewProductIntoExistingCapabilityAttributeList(capabilityAggregate, item, product);
                    }
                }
                
            }

            this.AddNewCapabilityAndProductIntoCapabilityAttributeList(capabilityAggregates, capability, product);
        }

        private void AddNewProductIntoExistingCapabilityAttributeList(
            CapabilityAggregate capabilityAggregate, 
            CapabilityAttribute capabilityAttribute, 
            ProductAggregate product)
        {
            capabilityAggregate.AddNewProductCapabilityAttributeToTheList(product.product, capabilityAttribute.Value);
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
            ProductAggregate product)
        {
            CapabilityAggregate ca = new CapabilityAggregate();
            foreach (var item in capability.capabilityAttributes.attributes)
            {
                ca.CapabilityId = capability.Id;
                ca.Attribute = item.Name;
                ca.AddNewProductCapabilityAttributeToTheList(product.product, item.Value);
            }
            capabilityAggregates.Add(ca);

        }
    }
}
