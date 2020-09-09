using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class File : BaseActiveModel, INamedModel
    {
        public File() { }

        public File(FileCreateDto file)
        {
            // TODO: Finish Constructor
            FileLabel = file.FileLabel;
            Active = true;
        }

        // Internal ID from the Programs, consisting of the 3-digit county number 
        // and a 4-digit system-generated sequential number for each county (xxx-xxxx)
        [StringLength(9)]
        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        public List<Cabinet> Cabinets { get; set; }

        public List<Facility> Facilities { get; set; }

        public string Name => FileLabel;
    }
}
