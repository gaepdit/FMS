using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FMS
{
    public class JsonService
    {
        // Asks Webhost (Web Server) where to find the Json
        // webHostEnvironment is a service shared to my JsonService
        public JsonService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // WebHostEnvironment stores the location of the Json Data
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonPath
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", ""); }
        }

        // deserializing the text string into the object
        // IEnumerable is the Base of List 
        // (IEnumerable is anything you can do a for loop through)
        // anything you can enumerate through
        //public IEnumerable<Facility> GetFacilty()
        //{
        //    // create a reader and open the Json file
        //    using (var jsonFileReader = Facility.OpenText(JsonPath))
        //    {
        //        // deserialize data in reader into an array of products to the end of file
        //        return JsonSerializer.Deserialize<Facility[]>(JsonService(),
        //            // Optional arguments for the Json Serializer
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });
        //    }
        //}
    }
}