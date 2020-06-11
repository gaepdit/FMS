﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using FMS.Models;

namespace FMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPDProgramController : ControllerBase
    {
        // GET: api/<EPDProgramController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EPDProgramController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EPDProgramController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EPDProgramController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EPDProgramController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}