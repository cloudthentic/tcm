using System;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace tcm.processor.app.ctest
{
    public class ProcessorTest
    {

        private IConfigurationRoot GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.development.json")
                .Build();
            return config;
        }

        [Theory]
        [InlineData("../../../../../products/azure")]
        public void ProcessorAppTest(object path)
        {
            var connection = this.GetConfiguration()["tableStorageConnection"];
            var processor = new processor.app.Processor(connection);
            var result = processor.ProcessAll(path as string);

            Assert.NotNull(result);
            Assert.True(result.Count == 8);

        }


    }
}
