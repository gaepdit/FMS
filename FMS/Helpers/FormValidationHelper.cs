using FMS.Domain.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

namespace FMS.Helpers
{
    public class FormValidationHelper
    {
        private static readonly ModelErrorCollection errCol = [];
        private static readonly DateOnly minDate = new(1990, 1, 1);
        private static readonly DateOnly maxDate = DateOnly.FromDateTime(DateTime.Today);
        private static readonly string rnPattern = @"^\bRN\d{4}$";
        private static readonly string rnPatternCmplt = @"^\bRN\d{6}$";
        private static readonly string hsiPattern = @"^\d{5}$";
        private static readonly Regex rnRegex = new(rnPattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
        private static readonly Regex rnCmpltRegex = new(rnPatternCmplt, RegexOptions.None, TimeSpan.FromMilliseconds(100));
        private static readonly Regex hsiRegex = new(hsiPattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "<Pending>")]
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
                if (facility.RNDateReceived is null)
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.dateReceivedMissing)));
                }
                else if (facility.RNDateReceived > maxDate)
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.dateReceivedMax)));
                }
                else if (facility.RNDateReceived < minDate)
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.dateReceivedMin)));
                }
                // Check Facility Number
                if (facility.FacilityNumber != null
                    && facility.FacilityStatusName != "COMPLAINT"
                    && !rnRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.facilityNumberRNInvalid)));
                }
                // Check Facility Number for COMPLAINT status
                if (facility.FacilityNumber != null
                    && facility.FacilityStatusName == "COMPLAINT"
                    && !rnCmpltRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.facilityNumberRNComplaintInvalid)));
                }
                if (facility.FacilityNumber == null
                    && facility.FacilityStatusName == "COMPLAINT")
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.facilityNumberRNComplaintInvalid)));
                }
                // Check HSI Number 
                if (!string.IsNullOrEmpty(facility.HSInumber) && !hsiRegex.IsMatch(facility.HSInumber))
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.hsiNumberInvalid)));
                }
            }
            else if (facility.FacilityTypeName == "HSI")
            {
                //Check Facility Number
                if (!string.IsNullOrEmpty(facility.FacilityNumber) && !hsiRegex.IsMatch(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.facilityNumberHSIInvalid)));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(facility.FacilityNumber))
                {
                    errCol.Add(new ModelError(GetErrMessage(ValidationErrorMessages.facilityNumberMissing)));
                }
            }

            return errCol;
        }

        public enum ValidationErrorMessages
        {
            dateReceivedMissing,
            dateReceivedMax,
            dateReceivedMin,
            facilityNumberRNInvalid,
            facilityNumberRNComplaintInvalid,
            hsiNumberInvalid,
            facilityNumberHSIInvalid,
            facilityNumberMissing
        }

        public static string GetErrMessage(ValidationErrorMessages validationError)
        {
            var valErr = validationError;
            var msgString = string.Empty;

            switch (valErr)
            {
                case ValidationErrorMessages.dateReceivedMissing:
                    msgString = string.Concat("Facility.RNDateReceived", "^", "Date Received must be entered.");
                    break;
                case ValidationErrorMessages.dateReceivedMax:
                    msgString = string.Concat("Facility.RNDateReceived", "^", "Date must not be beyond today.");
                    break;
                case ValidationErrorMessages.dateReceivedMin:
                    msgString = string.Concat("Facility.RNDateReceived", "^", "Date must not be before 1/1/1990.");
                    break;
                case ValidationErrorMessages.facilityNumberRNInvalid:
                    msgString = string.Concat("Facility.FacilityNumber", "^", "Facility Number must be in the form 'RNdddd'");
                    break;
                case ValidationErrorMessages.facilityNumberRNComplaintInvalid:
                    msgString = string.Concat("Facility.FacilityNumber", "^", "Facility Number must be in the form 'RNdddddd' for COMPLAINT status.");
                    break;
                case ValidationErrorMessages.hsiNumberInvalid:
                    msgString = string.Concat("Facility.HSInumber", "^", "HSI Number must be 5 digits Only.");
                    break;
                case ValidationErrorMessages.facilityNumberHSIInvalid:
                    msgString = string.Concat("Facility.FacilityNumber", "^", "HSI Number must be 5 digits Only.");
                    break;
                case ValidationErrorMessages.facilityNumberMissing:
                    msgString = string.Concat("Facility.FacilityNumber", "^", "Facility Number must not be blank");
                    break;
                default:
                    break;
            }

            return msgString;
        }
    }
}
