using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Dto
{
    public class FacilitySummaryDto
    {
        public FacilitySummaryDto(Facility facility)
        {
            Id = facility.Id;
            FileLabel = facility.File.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Cabinets = facility.File.CabinetFiles?
                .Select(c => c.Cabinet.Name).ToList()
                ?? new List<string>();
            RetentionRecords = facility.RetentionRecords?
                .Select(e => new RetentionRecordSummaryDto(e)).ToList()
                ?? new List<RetentionRecordSummaryDto>();
        }

        public Guid Id { get; set; }

        [Display(Name = "File")]
        public string FileLabel { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; set; }

        public List<string> Cabinets { get; set; }

        public List<RetentionRecordSummaryDto> RetentionRecords { get; set; }
    }
}