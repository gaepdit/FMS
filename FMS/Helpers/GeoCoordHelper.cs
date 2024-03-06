using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FMS
{
    public static class GeoCoordHelper
    {
        private const decimal UpperLat = 35.0m;
        private const decimal LowerLat = 30.36m;
        private const decimal EasternLong = -80.84m;
        private const decimal WesternLong = -85.61m;

        public enum CoordinateValidation
        {
            [Description("Latitude entered is outside State of Georgia. Must be between 35.0 and 30.36 North Latitude or zero if unknown.")]
            LatNotInGeorgia = 1,
            [Description("Longitude entered is outside State of Georgia. Must be between -80.84 and -85.61 West Longitude or zero if unknown.")]
            LongNotInGeorgia = 2,
            Valid = 3
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

        public static bool ValidLat(decimal? lat) => lat <= UpperLat && lat >= LowerLat;

        public static bool ValidLong(decimal? lng) => lng <= EasternLong && lng >= WesternLong;

        public static CoordinateValidation ValidateCoordinates(decimal? lat, decimal? lng)
        {
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
