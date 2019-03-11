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

        public void ParseYaml(string path)
        {
            System.IO.TextReader tr = System.IO.File.OpenText(path);
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var yamlObject = deserializer.Deserialize(tr);

            var serializer = new Newtonsoft.Json.JsonSerializer();

            var ms = new System.IO.MemoryStream();
            var w = new System.IO.StreamWriter(ms);
            serializer.Serialize(w, yamlObject);

            ms.Seek(0, System.IO.SeekOrigin.Begin);
            System.IO.TextReader jtr = new System.IO.StreamReader(ms);
            Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(jtr);
            
            var result = serializer.Deserialize(reader);

        }
    }
}
