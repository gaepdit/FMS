using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FMS
{
    public static class GeoCoordHelper
    {
        private const decimal ZeroLatLong = 0.0m;
        private const decimal UpperLat = 35.0m;
        private const decimal LowerLat = 30.36m;
        private const decimal EasternLong = -80.84m;
        private const decimal WesternLong = -85.61m;

        public enum CoordinateValidation
        {
            [Description("Latitude and Longitude must both be zero (0) or both valid coordinates.")]
            BothNotZero = 1,
            [Description("Latitude entered is outside State of Georgia. Must be between 35.0 and 30.36 North Latitude or zero if unknown.")]
            LatNotInGeorgia = 2,
            [Description("Longitude entered is outside State of Georgia. Must be between -80.84 and -85.61 West Longitude or zero if unknown.")]
            LongNotInGeorgia = 3,
            Valid = 4
        }

        public static string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        private static bool ValidLat(decimal? lat) => lat is ZeroLatLong or null || (lat <= UpperLat && lat >= LowerLat);

        private static bool ValidLong(decimal? lng) => lng is ZeroLatLong or null || (lng <= EasternLong && lng >= WesternLong);

        private static bool BothZeroOrBothNonzero(decimal? lat, decimal? lng) => (lat == null && lng == null) || (lat == 0 && lng == 0) || (lat != 0 && lng != 0);

        public static CoordinateValidation ValidateCoordinates(decimal? lat, decimal? lng)
        {
            if (!BothZeroOrBothNonzero(lat, lng))
            {
                return CoordinateValidation.BothNotZero;
            }
            if (!ValidLat(lat))
            {
                return CoordinateValidation.LatNotInGeorgia;
            }
            if (!ValidLong(lng))
            {
                return CoordinateValidation.LongNotInGeorgia;
            }
            return CoordinateValidation.Valid;
        }
    }
}
