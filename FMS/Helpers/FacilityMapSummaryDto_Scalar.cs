using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClosedXML.Attributes;

namespace FMS.Domain.Dto.Facility
{
    public class FacilityMapSummaryDto_Scalar
    {
        public FacilityMapSummaryDto_Scalar(FacilityMapSummaryDto mapSummaryDto)
            
        {
            FacilityNumber = mapSummaryDto.FacilityNumber;
            Name = mapSummaryDto.Name;
            Active = mapSummaryDto.Active;
            FacilityStatus = mapSummaryDto.FacilityStatus;
            FacilityType = mapSummaryDto.FacilityType;
            FileLabel = mapSummaryDto.FileLabel;
            Address = mapSummaryDto.Address;
            City = mapSummaryDto.City;
            State = mapSummaryDto.State;
            PostalCode = mapSummaryDto.PostalCode;
            Latitude = mapSummaryDto.Latitude;
            Longitude = mapSummaryDto.Longitude;
            Distance = mapSummaryDto.Distance;
        }
        public Guid Id { get; set; }

        [XLColumn(Header= "Facility Number")]
        public string FacilityNumber { get; set; }

        [XLColumn(Header= "Facility Name")]
        public string Name { get; set; }

        [XLColumn(Header= "Active Site")]
        public bool Active { get; set; } = true;

        [XLColumn(Header= "Facility Status")]
        public string FacilityStatus { get; set; }

        [XLColumn(Header= "Type/Environmental Interest")]
        public string FacilityType { get; set; }

        [XLColumn(Header= "File Label")]
        public string FileLabel { get; set; }

        [XLColumn(Header= "Address")]
        public string Address { get; set; }

        [XLColumn(Header= "City")]
        public string City { get; set; }

        [XLColumn(Header= "State")]
        public string State { get; set; }

        [XLColumn(Header= "ZIP Code")]
        public string PostalCode { get; set; }

        [XLColumn(Header= "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        [Column(TypeName = "decimal(8, 6)")]
        public decimal Latitude { get; set; }

        [XLColumn(Header= "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        [XLColumn(Header= "Distance")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Distance { get; set; }
    }
}
