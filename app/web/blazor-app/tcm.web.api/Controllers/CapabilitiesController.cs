using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web.Http.Cors;

namespace tcm.web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapabilitiesController : ControllerBase
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");

        // GET: api/Capabilities
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Capabilities/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("tcmapp");

            TableOperation operation = TableOperation.Retrieve<Model.CapabilityEntity>("capability", id);

            TableResult result = table.ExecuteAsync(operation).Result;
            var capability = result.Result as Model.CapabilityEntity;

            return capability.products;
        }

        // POST: api/Capabilities
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Capabilities/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
