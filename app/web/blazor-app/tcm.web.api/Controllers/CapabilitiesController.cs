using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web.Http.Cors;
using Microsoft.Extensions.Configuration;
using tcm.processor.adapter.tableStorage;

namespace tcm.web.api.Controllers
{

    public class CapabilityEntity
    {
        public CapabilityEntity(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CapabilitiesController : ControllerBase
    {
        IConfiguration config;

        public CapabilitiesController(IConfiguration configuration)
        {
            this.config = configuration;
        }
        

        // GET: api/Capabilities
        [HttpGet]
        public string Get()
        {
            var entity1 = new CapabilityEntity("Network|0.1", "Network|0.1");
            var entity2 = new CapabilityEntity("SLA|0.1", "SLA|0.1");
            var entity3 = new CapabilityEntity("Security|0.1", "Security|0.1");
            List<CapabilityEntity> list = new List<CapabilityEntity>();
            list.Add(entity1);
            list.Add(entity2);
            list.Add(entity3);

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return result;
        }

        // GET: api/Capabilities/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {

            var connection = this.config["apiConfiguration:tableStorageConnection"] as string;
            TableStorageReaderAdapter adapter = new TableStorageReaderAdapter(connection);

            var results = adapter.ReadCapabilities(id);

            return results;

        }


    }
}
