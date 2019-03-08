using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web.Http.Cors;

namespace tcm.web.api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://localhost:1072", headers: "*", methods: "*")]
    public class ValuesController : ControllerBase
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tcmappstorage;AccountKey=qPPnZg/munGpCTXhPe9ypvfhns557CUuYvA4be53NzP+wEc/zs6XASWAlHqMLpG4z9TltL4/LEajviZV6uspvQ==;EndpointSuffix=core.windows.net");

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ResponseCache(Duration = 10)]
        public ActionResult<string> Get(string id)
        {
            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("tcmapp");

            string partitionKey = id.Split('|')[0];
            string rowKey = id.Split('|')[1];

            TableOperation operation = TableOperation.Retrieve<Model.CapabilityEntity>(partitionKey, rowKey);

            TableResult result = table.ExecuteAsync(operation).Result;
            var capability = result.Result as Model.CapabilityEntity;

            return capability.products;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
