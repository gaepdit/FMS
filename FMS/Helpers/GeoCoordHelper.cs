namespace FMS
{
    public static class GeoCoordHelper
    {
        private const decimal ZeroLatLong = 0.0m;
        public const decimal UpperLat = 35.0m;
        public const decimal LowerLat = 30.36m;
        public const decimal EasternLong = -80.84m;
        public const decimal WesternLong = -85.61m;

        public static bool ValidLat(decimal? lat) => lat is ZeroLatLong or null || (lat <= UpperLat && lat >= LowerLat);

        public static bool ValidLong(decimal? lng) => lng is ZeroLatLong or null || (lng <= EasternLong && lng >= WesternLong);

        public static bool BothZeroOrBothNonzero(decimal? lat, decimal? lng) => (lat == null && lng == null) || (lat == 0 && lng == 0) || (lat != 0 && lng != 0);
    }
}
