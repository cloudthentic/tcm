using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace tcm.processor.adapter.tableStorage.utest
{
    public class TableStorageReaderAdapterTest
    {

        private IConfigurationRoot GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.development.json")
                .Build();
            return config;
        }

        [Fact]
        public void ReadProductsTest()
        {
            var connection = this.GetConfiguration()["tableStorageConnection"];
            TableStorageReaderAdapter adapter = new TableStorageReaderAdapter(connection);
            var results = adapter.ReadProductEntities("Network|0.1", "Virtual-Network-Integration");
            Assert.NotNull(results);
        }

        [Fact]
        public void ReadCapabilitiesTest()
        {
            var connection = this.GetConfiguration()["tableStorageConnection"];
            TableStorageReaderAdapter adapter = new TableStorageReaderAdapter(connection);
            var results = adapter.ReadCapabilities("Network|0.1");
            Assert.NotNull(results);
        }
    }
}
