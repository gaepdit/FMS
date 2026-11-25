using FMS.Domain.Repositories;

namespace FMS.Helpers
{
    public class MapHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILocationRepository _locationRepository;
        private readonly string MapDirectory;
        private readonly string GoogleMapsApiKey;
        private readonly string MapDrive;
        private readonly string AppPath;
        public MapHelper(IConfiguration configuration, ILocationRepository locationRepository)
        {
            _configuration = configuration;
            _locationRepository = locationRepository;
            MapDrive = _configuration["StorageSettings:MapDrive"];
            AppPath = _configuration["StorageSettings:AppPath"];
            MapDirectory = _configuration["StorageSettings:MapDirectory"];
            GoogleMapsApiKey = _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;
        }

        public async Task<string> GetMapImage(MapProperties mapProperties)
        {
            if(mapProperties.UpdateMapProperties)
            {
                await _locationRepository.UpdateMapPropertiesAsync(mapProperties.LocationId, mapProperties.MapType, mapProperties.MapZoom);
            }

            var imagePath = GetStoredMapImagePath(mapProperties);

            if (System.IO.File.Exists(imagePath) && !mapProperties.Edited)
            {
                return imagePath;
            }
            else
            {
                // If the image doesn't exist, generate it and save it
                var success = await SaveStoredMapImage(mapProperties);
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
                Directory.CreateDirectory(directoryPath);
                await File.WriteAllBytesAsync(imagePath, imageBytes);
                return true;
            }
            catch (HttpRequestException)
            {               
                return false;
            }
            catch (IOException)
            {
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
        public MapProperties(Guid locationId, string facilityNumber, decimal latitude, decimal longitude, string mapZoom, string mapType, bool edited, bool updateMapProperties)
        {
            LocationId = locationId;
            FacilityNumber = facilityNumber;
            Latitude = latitude;
            Longitude = longitude;
            MapZoom = mapZoom ?? "15";
            MapType = mapType ?? "roadmap";
            Edited = edited;
            UpdateMapProperties = updateMapProperties;
        }

        public Guid LocationId { get; set; }
        public string FacilityNumber { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string MapZoom { get; set; }
        public string MapType { get; set; }
        public bool Edited { get; set; }
        public bool UpdateMapProperties { get; set; }
    }
}
  