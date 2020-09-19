using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Dto
{
    public class FacilityMapSummaryDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }       
       
        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Display(Name = "Facility Status")]
        public string FacilityStatus { get; set; }

        [Display(Name = "Facility Type")]
        public string FacilityType { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }
        public Guid FileId { get; set; }

        [Display(Name = "Address")]
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
        public decimal Distance { get; set; }
        public string FullAddress { get { return Address + ", " + City + ", " + State + " " + PostalCode; } }
    }
}