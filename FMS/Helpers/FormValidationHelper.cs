using FMS.Domain.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.RegularExpressions;

namespace FMS.Helpers
{
    public static class FormValidationHelper
    {
        public static ModelErrorCollection ValidateFacilityEditForm(FacilityEditDto facility)
        {
            ModelErrorCollection errCol = [];
            DateOnly minDate = new DateOnly(1990, 1, 1);
            DateOnly maxDate = DateOnly.FromDateTime(DateTime.Today);
            string rnPattern = @"^\bRN\d{4}$";
            string hsiPattern = @"^\d{5}$";
            Regex rnRegex = new(rnPattern);
            Regex hsiRegex = new(hsiPattern);

            // Make sure GeoCoordinates are withing the State of Georgia or both Zero
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
                if (!rnRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.FacilityNumber", "^", "Facility Number must be in the form 'RNdddd'")));
                }
                // Check HSI Number 
                if (!facility.HSInumber.IsNullOrEmpty() && !hsiRegex.IsMatch(facility.HSInumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.HSInumber", "^", "HSI Number must be 5 digits Only.")));
                }
            }

            return errCol;
        }

        public static ModelErrorCollection ValidateFacilityAddForm(FacilityCreateDto facility)
        {
            ModelErrorCollection errCol = [];
            DateOnly minDate = new DateOnly(1990, 1, 1);
            DateOnly maxDate = DateOnly.FromDateTime(DateTime.Today);
            string rnPattern = @"^\bRN\d{4}$";
            string hsiPattern = @"^\d{5}$";
            Regex rnRegex = new(rnPattern);
            Regex hsiRegex = new(hsiPattern);

            // Make sure GeoCoordinates are withing the State of Georgia or both Zero
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
                if (facility.RNDateReceived is null)
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
                if (!rnRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.FacilityNumber", "^", "Facility Number must be in the form 'RNdddd'")));
                }
                // Check HSI Number 
                if (!facility.HSInumber.IsNullOrEmpty() && !hsiRegex.IsMatch(facility.HSInumber))
                {
                    errCol.Add(new ModelError(string.Concat("Facility.HSInumber", "^", "HSI Number must be 5 digits Only.")));
                }
            }

            return errCol;
        }
    }
}
