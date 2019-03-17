using System;
using System.Collections.Generic;
using tcm.processor.model;
using Microsoft.ApplicationInsights;

namespace tcm.processor.adapter.fileSystem
{
    /// <summary>
    /// Ports-adapter arch Adapter for processing hiearchy of product description files
    /// </summary>
    public class FileSystemReaderAdapter
    {

        public FileSystemReaderAdapter()
        {

        }

        /// <summary>
        /// Parse file system from given path and return list of products dicovered in the faile system
        /// </summary>
        /// <remarks>
        /// Product failes are YAML files conforming to defined schema. Schema (informal) are defined in 'Capabilities' hiearchy
        /// </remarks>
        /// <param name="path"></param>
        /// <returns>List of Product aggregates</returns>
        public IList<ProductAggregate> ParseFileSystemWithProductHiearchy(string path)
        {
            List<ProductAggregate> productList = new List<ProductAggregate>();
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(path);

            this.ProcessDirectory(info, productList);
            return productList;
        }

        private void ProcessDirectory(System.IO.DirectoryInfo directoryInfo, IList<ProductAggregate> productList)
        {

            if(directoryInfo.GetDirectories() != null)
            {
                this.ProcessFilesInDirectory(directoryInfo, productList);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    
                    this.ProcessDirectory(directory, productList);
                }
            }
        }

        private void ProcessFilesInDirectory(System.IO.DirectoryInfo directory, IList<ProductAggregate> productList)
        {
            foreach(var file in directory.GetFiles())
            {
                var productAggregate = this.ParseYaml(file.FullName);
                productList.Add(productAggregate);
            }
        }

        public ProductAggregate ParseYaml(string path)
        {
            System.IO.TextReader tr = System.IO.File.OpenText(path);
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var yamlObject = deserializer.Deserialize(tr);

            Type type = yamlObject.GetType();

            var obj = (yamlObject as System.Collections.Generic.Dictionary<object, object>);

            var productAggregate = this.YamlObjToProductAggregate(obj);

            return productAggregate;

        }

        private ProductAggregate YamlObjToProductAggregate(System.Collections.Generic.Dictionary<object, object> yamlObj)
        {
            var productAggregate = new ProductAggregate();
            productAggregate.product = yamlObj["product"] as string;
            productAggregate.productId = yamlObj["product-id"] as string;

            var capabilities = yamlObj["capabilities"] as List<object>;
            foreach(var capability in capabilities)
            {
                var items = capability as Dictionary<object, object>;
                var values = items["capability"] as Dictionary<object, object>;

                string capabilityId = values["id"] as string;
                Capability cap = productAggregate.productCapabilities.AddNewCapability(capabilityId);

                // add attributes
                foreach(var capItem in values["capability-attributes"] as Dictionary<object, object>)
                {
                    cap.capabilityAttributes.AddNewAttribute(capItem.Key as string, capItem.Value as string);
                }
            }
            
            return productAggregate;
        }
    }
}
