namespace FMS
{
    public static class GeoCoordHelper
    {
        private const decimal NullLatLong = 0.0m;
        public const decimal UpperLat = 35.0m;
        public const decimal LowerLat = 30.36m;
        public const decimal EasternLong = -80.84m;
        public const decimal WesternLong = -85.61m;

        public static bool ValidLat(decimal lat) => lat is NullLatLong or <= UpperLat and >= LowerLat;

        public static bool ValidLong(decimal lng) => lng is NullLatLong or <= EasternLong and >= WesternLong;

        public static bool BothZeroOrBothNonzero(decimal lat, decimal lng) => (lat == 0 && lng == 0) || (lat != 0 && lng != 0);
    }
}
