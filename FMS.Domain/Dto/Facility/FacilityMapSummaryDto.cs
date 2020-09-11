using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityMapSummaryDto
    {  
        public FacilityMapSummaryDto(Facility facility)
        {         
            Id = facility.Id;
            FileLabel = facility.File.FileLabel;
            FileId = facility.File.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            FacilityStatus = facility.FacilityStatus;
            FacilityType = facility.FacilityType;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;         
        }          

        public Guid Id { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }       
       
        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Display(Name = "Facility Status")]
        public FacilityStatus FacilityStatus { get; set; }

        [Display(Name = "Facility Type")]
        public FacilityType FacilityType { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }
        public Guid FileId { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; set; }

        [Display(Name = "Distance")]
        public decimal? Distance { get; set; }      

        public string? FullAddress { get { return Address + City + ", " + State + " "  + PostalCode; } }
    }
}