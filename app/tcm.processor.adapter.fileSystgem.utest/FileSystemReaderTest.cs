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
    }
}
