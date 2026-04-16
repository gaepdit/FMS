namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Map Type")]
        public string MapType { get; set; }

        [Display(Name = "Map Zoom")]
        public string MapZoom { get; set; }
    }
}
