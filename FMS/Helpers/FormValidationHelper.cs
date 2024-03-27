using FMS.Domain.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.RegularExpressions;

namespace FMS.Helpers
{
    public static class FormValidationHelper
    {
        private static readonly ModelErrorCollection errCol = [];
        private static readonly DateOnly minDate = new(1990, 1, 1);
        private static readonly DateOnly maxDate = DateOnly.FromDateTime(DateTime.Today);
        private static readonly string rnPattern = @"^\bRN\d{4}$";
        private static readonly string hsiPattern = @"^\d{5}$";
        private static readonly Regex rnRegex = new(rnPattern);
        private static readonly Regex hsiRegex = new(hsiPattern);

        public static ModelErrorCollection ValidateFacilityEditForm(FacilityEditDto facilityEditDto)
        {
            FacilityValidationDtoScalar facilityValidation = new(facilityEditDto);
            return ValidateFacilityAddEditForms(facilityValidation);
        }

        public static ModelErrorCollection ValidateFacilityAddForm(FacilityCreateDto facilityCreateDto)
        {
            FacilityValidationDtoScalar facilityValidation = new(facilityCreateDto);    
            return ValidateFacilityAddEditForms(facilityValidation);
        }

        public static ModelErrorCollection ValidateFacilityAddEditForms(FacilityValidationDtoScalar facility)
        {
            errCol.Clear();

            // Make sure GeoCoordinates are withing the State of Georgia
            GeoCoordHelper.CoordinateValidation EnumVal = GeoCoordHelper.ValidateCoordinates(facility.Latitude, facility.Longitude);

            string ValidationString = GeoCoordHelper.GetDescription(EnumVal);

            if (EnumVal != GeoCoordHelper.CoordinateValidation.Valid)
            {
                if (EnumVal == GeoCoordHelper.CoordinateValidation.LongNotInGeorgia)
                {
                    errCol.Add(new ModelError(string.Concat("Facility.Longitude", "^", ValidationString)));
                }
                else
                {
                    errCol.Add(new ModelError(string.Concat("Facility.Latitude", "^", ValidationString)));
                }
            }

            // Check all things related to Release Notifications
            if (facility.FacilityTypeName == "RN")
            {
                // Check Date Received
                if ( facility.RNDateReceived is null)
                {
                    errCol.Add(new ModelError(string.Concat("Facility.RNDateReceived", "^", "Date Received must be entered.")));
                }
                else if (facility.RNDateReceived > maxDate)
                {
                    errCol.Add(new ModelError(string.Concat("Facility.RNDateReceived", "^", "Date must not be beyond today.")));
                }
                else if (facility.RNDateReceived < minDate)
                {
                    errCol.Add(new ModelError(string.Concat("Facility.RNDateReceived", "^", "Date must not be before 1/1/1990.")));
                }
                // Check Facility Number
                if (facility.FacilityNumber != null && !rnRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.FacilityNumber", "^", "Facility Number must be in the form 'RNdddd'")));
                }
                // Check HSI Number 
                if (!facility.HSInumber.IsNullOrEmpty() && !hsiRegex.IsMatch(facility.HSInumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.HSInumber", "^", "HSI Number must be 5 digits Only.")));
                }
            }
            else if(facility.FacilityTypeName == "HSI")
            {
                // Check Facility Number 
                if (!facility.FacilityNumber.IsNullOrEmpty() && !hsiRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.FacilityNumber", "^", "HSI Number must be 5 digits Only.")));
                }
            }
            else
            {
                if (facility.FacilityNumber.IsNullOrEmpty())
                {
                    errCol.Add(new ModelError(string.Concat("Facility.FacilityNumber", "^", "Facility Number must not be blank")));
                }
            }

            return errCol;
        }
    }
}