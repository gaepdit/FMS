using FMS.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FMS
{
    public class JsonSearchService
    {
        // Asks Webhost (Web Server) where to find the Json
        // webHostEnvironment is a service shared to my JsonService
        public JsonSearchService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // WebHostEnvironment stores the location of the Json Data
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonPathFacility
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "JSONdata", "DevFacilities.json"); }
        }

        private string JsonPathCounties
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "JSONdata", "DevCounty.json"); }
        }

        // deserializing the text string into the object
        // IEnumerable is the Base of List 
        // (IEnumerable is anything you can do a FOR loop through)
        // anything you can enumerate through
        public IEnumerable<Facility> GetFacilties()
        {
            // create a reader and open the Json file
            using (var jsonFileReader = System.IO.File.OpenText(JsonPathFacility))
            {
                // deserialize data in reader into an array of products to the end of file
                return JsonSerializer.Deserialize<Facility[]>(jsonFileReader.ReadToEnd());
                //,
                //    // Optional arguments for the Json Serializer
                //    new JsonSerializerOptions
                //    {
                //        PropertyNameCaseInsensitive = true
                //    });
            }
        }

        public IEnumerable<County> GetCounties()
        {
            using (var jsonFileReader = System.IO.File.OpenText(JsonPathCounties))
            {
                return JsonSerializer.Deserialize<County[]>(jsonFileReader.ReadToEnd());
            }
        }
    }
}