using System;
using Xunit;

namespace tcm.processor.adapter.fileSystem.utest
{
    public class FileSystemReaderTest
    {
        [Fact]
        public void ParseYamlTest()
        {
            tcm.processor.adapter.fileSystem.FileSystemReaderAdapter reader = new FileSystemReaderAdapter();
            reader.ParseYaml("./TestData/singleFile/azure-cosmos-db.yml");
        }

        [Fact]
        public void ParseFileSystemWithProductHiearchyTest()
        {
            tcm.processor.adapter.fileSystem.FileSystemReaderAdapter reader = new FileSystemReaderAdapter();
            var results = reader.ParseFileSystemWithProductHiearchy("./TestData/singleFile");
            Assert.NotNull(results);
            Assert.True(results.Count == 1);
        }

        [Fact]
        public void ParseFileSystemWithProductHiearchyTest_Hiearchy()
        {
            tcm.processor.adapter.fileSystem.FileSystemReaderAdapter reader = new FileSystemReaderAdapter();
            var results = reader.ParseFileSystemWithProductHiearchy("./TestData/hiearchy");
            Assert.NotNull(results);
            Assert.True(results.Count == 2, "Expected 2 products - failed");
        }
    }
}
