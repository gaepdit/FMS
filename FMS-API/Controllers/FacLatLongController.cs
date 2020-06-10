using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacLatLongController : ControllerBase
    {
        // GET: api/<FacLatLongController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FacLatLongController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacLatLongController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacLatLongController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacLatLongController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
