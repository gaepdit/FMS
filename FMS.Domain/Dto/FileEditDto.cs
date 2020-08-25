using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileEditDto
    {
        public FileEditDto() { }

        public FileEditDto(File file)
        {
            Id = file.Id;
            Active = file.Active;
            FileLabel = file.FileLabel;
            FileCabinets = file.FileCabinets;
            Facilities = file.Facilities;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "File Label")]
        [StringLength(9)]
        public string FileLabel { get; set; }

        public List<FileCabinet> FileCabinets { get; set; }

        public List<Facility> Facilities { get; set; }
    }
}
