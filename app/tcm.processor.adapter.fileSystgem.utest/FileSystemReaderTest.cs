using System;
using Xunit;

namespace tcm.processor.adapter.fileSystem.utest
{
    public class FileSystemReaderTest
    {
        [Fact]
        public void ParseYamlTest()
        {
            tcm.processor.adapter.fileSystem.FileSystemReader reader = new FileSystemReader();
            reader.ParseYaml("./TestData/azure-cosmos-db.yml");
        }

        [Fact]
        public void YamlObjToProductAggregateTest()
        {
            tcm.processor.adapter.fileSystem.FileSystemReader reader = new FileSystemReader();
            var result = reader.ParseYaml("./TestData/azure-cosmos-db.yml");
            var product = reader.YamlObjToProductAggregate(result);
        }
    }
}
