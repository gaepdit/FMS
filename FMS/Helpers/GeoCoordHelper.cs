namespace FMS
{
    public static class GeoCoordHelper
    {
        public const decimal nullLatLong = 0.0m;
        public const decimal upperLat = 35.0m;
        public const decimal lowerLat = 30.36m;
        public const decimal easternLong = -80.84m;
        public const decimal westernLong = -85.61m;

        public static bool InvalidLat(decimal Lat) => _ = Lat != nullLatLong && (Lat > upperLat || Lat < lowerLat);

        public static bool InvalidLong(decimal Long) => _ = Long != nullLatLong && (Long > easternLong || Long < westernLong);
    }
}