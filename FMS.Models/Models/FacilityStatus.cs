namespace FMS.Models.Models
{
    public class FacilityStatus : BaseActiveModel
    {
        public string Status { get; set; }

        public EnvironmentalInterest EnvironmentalInterest { get; set; }
    }
}