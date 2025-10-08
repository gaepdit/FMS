using FMS.Domain.Dto;
using System;

namespace FMS.Helpers
{
    public class FacilityValidationDtoScalar
    {
        public FacilityValidationDtoScalar() { }

        public FacilityValidationDtoScalar(FacilityCreateDto facility)
        {
            FileLabel = facility.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            FacilityTypeName = facility.FacilityTypeName;
            FacilityStatusName = facility.FacilityStatusName;
            Latitude = (decimal)facility.Latitude;
            Longitude = (decimal)facility.Longitude;
            HSInumber = facility.HSInumber;
            RNDateReceived = facility.RNDateReceived;
        }

        public FacilityValidationDtoScalar(FacilityEditDto facility) 
        {
            FileLabel = facility.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            FacilityTypeName = facility.FacilityTypeName;
            FacilityStatusName = facility.FacilityStatusName;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            HSInumber = facility.HSInumber;
            RNDateReceived = facility.RNDateReceived;
        }

        public string FacilityNumber { get; set; }

        public string Name { get; set; }

        public string FileLabel { get; set; }

        public string FacilityTypeName { get; set; }

        public string FacilityStatusName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool IsRetained { get; set; }

        // The following properties only apply to Release Notifications
        public string HSInumber { get; set; }

        public DateOnly? RNDateReceived { get; set; }

    }
}
