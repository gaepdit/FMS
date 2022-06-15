namespace FMS
{
    public static class GeoCoordHelper
    {
        private const decimal NULL_LAT_LONG = 0.0m;
        private const decimal UPPER_LAT = 35.0m;
        private const decimal LOWER_LAT = 30.36m;
        private const decimal EASTERN_LONG = -80.84m;
        private const decimal WESTERN_LONG = -85.61m;

        public static bool InvalidLat(decimal lat) => _ = lat != NULL_LAT_LONG && (lat > UPPER_LAT || lat < LOWER_LAT);

        public static bool InvalidLong(decimal lng) => _ = lng != NULL_LAT_LONG && (lng > EASTERN_LONG || lng < WESTERN_LONG);

        public static bool BothZeros(decimal lat, decimal lng) => _ = (lat == 0 && lng == 0) || (lat != 0 && lng != 0);
    }
}