using System.ComponentModel.DataAnnotations;

namespace FMS.Models.Models
{
    public class FileCabinet : BaseActiveModel
    {
        // Existing Program Cabinet Numbers
        [StringLength(5)]
        public string Name { get; set; }

        public County StartCounty { get; set; }

        public County EndCounty { get; set; }

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }

        // Collection of Files in Cabinet go here if necessary
    }
}
