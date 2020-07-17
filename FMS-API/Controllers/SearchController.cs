using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchController(JsonSearchService facService)
        {
            this.FacService = facService;
        }

        public JsonSearchService FacService { get; }

        // GET: Facility
        [HttpGet]
        public IEnumerable<SearchModel> Get()
        {
            return (IEnumerable<SearchModel>)FacService.GetFacilties();
        }

        //// GET: api/Facility/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Facility
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Facility/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
