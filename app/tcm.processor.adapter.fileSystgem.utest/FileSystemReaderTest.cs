using System;
using Xunit;
using Microsoft.Extensions.Configuration;

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

        [Theory]
        [InlineData("../../../../../products/azure")]
        public void ParseFileSystemWithProductHiearchyTest_Hiearchy(object value)
        {
            tcm.processor.adapter.fileSystem.FileSystemReaderAdapter reader = new FileSystemReaderAdapter();
            var results = reader.ParseFileSystemWithProductHiearchy(value as string);
            Assert.NotNull(results);
            Assert.True(results.Count == 8, "Expected 2 products - failed");
        }

        [Fact]
        public void ReadConfigurationTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("tcm.processor.adapter.fileSystem.utest.xunit.runner.json")
                .Build();
            var result = config["integer-key"];
            Assert.NotNull(result);
            Assert.True((result as string) == "42");
        }
    }
}
