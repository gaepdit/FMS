using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FileCabinet : BaseActiveModel
    {
        // Existing Program Cabinet Numbers

        // File Cabinet Number
        [StringLength(5)]
        public string Name { get; set; }

        // Starting County
        public County StartCounty { get; set; }

        // Ending County
        public County EndCounty { get; set; }

        // Starting Sequence Number
        public int StartSequence { get; set; }

        // Ending Sequence Number
        public int EndSequence { get; set; }

        // Collection of Files in Cabinet go here if necessary
    }
}
