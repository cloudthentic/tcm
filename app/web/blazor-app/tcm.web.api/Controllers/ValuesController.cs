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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{capability}/{attribute}")]
        [ResponseCache(Duration = 10)]
        public ActionResult<string> Get(string capability, string attribute)
        {

            tcm.processor.adapter.tableStorage.TableStorageReaderAdapter ra = new processor.adapter.tableStorage.TableStorageReaderAdapter();
            var result = ra.ReadProduct(capability, attribute);
            return result;
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
