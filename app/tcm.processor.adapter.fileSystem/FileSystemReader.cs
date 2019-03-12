using System;
using System.Collections.Generic;
using tcm.processor.model;

namespace tcm.processor.adapter.fileSystem
{
    public class FileSystemReader
    {
        public IList<ProductAggregate> ParseFileSystemWithProductHiearchy(string path)
        {
            List<ProductAggregate> productList = new List<ProductAggregate>();
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(path);

            this.ProcessDirectory(info, productList);
            return productList;
        }

        private void ProcessDirectory(System.IO.DirectoryInfo directoryInfo, IList<ProductAggregate> productList)
        {
            while(directoryInfo.GetDirectories() != null || directoryInfo.GetDirectories().Length > 0)
            {
                foreach(var directory in directoryInfo.GetDirectories())
                {

                }
            }
        }

        private void ProcessFileInDirectory(System.IO.DirectoryInfo directory, IList<ProductAggregate> productList)
        {
            foreach(var file in directory.GetFiles())
            {
                
            }
        }

        public System.Collections.Generic.Dictionary<object, object> ParseYaml(string path)
        {
            System.IO.TextReader tr = System.IO.File.OpenText(path);
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var yamlObject = deserializer.Deserialize(tr);

            Type type = yamlObject.GetType();

            var obj = (yamlObject as System.Collections.Generic.Dictionary<object, object>);

            return obj;

        }

        public ProductAggregate YamlObjToProductAggregate(System.Collections.Generic.Dictionary<object, object> yamlObj)
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

                foreach(var capItem in values["capability-attributes"] as Dictionary<object, object>)
                {
                    var key = capItem.Key;
                }
            }
            
            return productAggregate;
        }
    }
}
