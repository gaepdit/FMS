using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileCabinetEditDto
    {
        public FileCabinetEditDto() { }
        public FileCabinetEditDto(FileCabinet fileCabinet)
        {
            Id = fileCabinet.Id;
            Active = fileCabinet.Active;
            Name = fileCabinet.Name;
            //StartCountyId = fileCabinet.StartCounty.Id;
            //EndCountyId = fileCabinet.EndCounty.Id;
            StartSequence = fileCabinet.StartSequence;
            EndSequence = fileCabinet.EndSequence;
        }

        public Guid Id;

        public bool Active { get; set; }

        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }

        //public Guid StartCountyId { get; set; }

        //public Guid EndCountyId { get; set; }

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }
    }
}
