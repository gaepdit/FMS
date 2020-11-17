using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FacilitySummaryDto
    {
        public FacilitySummaryDto(Facility facility)
        {
            Id = facility.Id;
            FileLabel = facility.File.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            FacilityType = facility.FacilityType;
            Name = facility.Name;
            Active = facility.Active;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            IsRetained = facility.IsRetained;
            Cabinets = new List<string>();
            RetentionRecords = facility.RetentionRecords?
                    .Select(e => new RetentionRecordSummaryDto(e)).ToList()
                ?? new List<RetentionRecordSummaryDto>();
        }

        public Guid Id { get; }

        [Display(Name = "File")]
        public string FileLabel { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Type/Environmental Interest")]
        public FacilityType FacilityType { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }

        [Display(Name = "Active Site")]
        public bool Active { get; }

        [Display(Name = "Street Address")]
        public string Address { get; }

        [Display(Name = "City")]
        public string City { get; }

        [Display(Name = "State")]
        public string State { get; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; }

        [Display(Name = "Cabinets")]
        public List<string> Cabinets { get; set; }

        [Display(Name = "Retention Records")]
        public List<RetentionRecordSummaryDto> RetentionRecords { get; }
    }
}