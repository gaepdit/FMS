namespace FMS.Helpers
{
    public class MapHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string MapDirectory;
        private readonly string GoogleMapsApiKey;
        private readonly string MapDrive;
        private readonly string AppPath;
        public MapHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            MapDrive = _configuration["StorageSettings:MapDrive"];
            AppPath = _configuration["StorageSettings:AppPath"];
            MapDirectory = _configuration["StorageSettings:MapDirectory"];
            GoogleMapsApiKey = _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;
        }

        public string GetMapImage(MapProperties mapProperties, bool edited = false)
        {
            var imagePath = GetStoredMapImagePath(mapProperties);

            if (System.IO.File.Exists(imagePath) && !edited)
            {
                return imagePath;
            }
            else
            {
                // If the image doesn't exist, generate it and save it
                var success = SaveStoredMapImage(mapProperties).Result;
                if (success)
                {
                    return imagePath;
                }
                else
                {
                    // Handle the case where the image couldn't be generated
                    return string.Empty; // or a placeholder image path
                }
            }
        }

        public async Task<bool> SaveStoredMapImage(MapProperties mapProperties)
        {
            var imageUrl = GetMapImageUrl(mapProperties);
            var imagePath = GetStoredMapImagePath(mapProperties);
            var directoryPath = GetMapDirPath();

            try
            {
                using var client = new HttpClient();
                byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
                System.IO.Directory.CreateDirectory(directoryPath); // Ensure the directory exists
                await System.IO.File.Create(imagePath).DisposeAsync();
                await System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);
                return true;
            }
            catch (HttpRequestException ex)
            {
                // Show the exception occured in The Page Message
                return false;
            }
        }

        public string GetMapDirPath() => Path.Combine(MapDrive, AppPath, MapDirectory);

        public string GetStoredMapImagePath(MapProperties mapProperties) => Path.Combine(MapDrive, AppPath, MapDirectory, $"{mapProperties.FacilityNumber}.png");

        public string GetMapImageUrl(MapProperties mapProperties)
        {
            var apiUrl = $"https://maps.googleapis.com/maps/api/staticmap?center={mapProperties.Latitude},{mapProperties.Longitude}&zoom={mapProperties.MapZoom}&size=250x250&format=png&markers=color:red|{mapProperties.Latitude},{mapProperties.Longitude}&maptype={mapProperties.MapType}&key={GoogleMapsApiKey}&style=feature:poi|visibility:off";
            return apiUrl;
        }
    }

    public class MapProperties
    {
        public MapProperties()
        {
            FacilityNumber = string.Empty;
            Latitude = 0;
            Longitude = 0;
            MapZoom = "15";
            MapType = "roadmap";
        }

        public MapProperties(string facilityNumber, decimal latitude, decimal longitude, string mapZoom, string mapType)
        {
            FacilityNumber = facilityNumber;
            Latitude = latitude;
            Longitude = longitude;
            MapZoom = mapZoom;
            MapType = mapType;
        }

        public string FacilityNumber { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string MapZoom { get; set; }
        public string MapType { get; set; }
    }
}
  