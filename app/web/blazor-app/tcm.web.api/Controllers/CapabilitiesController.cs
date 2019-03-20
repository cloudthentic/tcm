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

            var connection = this.config["apiConfiguration:tableStorageConnection"] as string;
            TableStorageReaderAdapter adapter = new TableStorageReaderAdapter(connection);

            var list = adapter.ReadCapabilityView();

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
