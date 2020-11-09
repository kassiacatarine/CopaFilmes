using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Championship.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        // GET: api/<StandingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StandingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StandingsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StandingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StandingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
