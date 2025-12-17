using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelTypeCreateDto
    {
        [Display(Name = "Parcel Type")]
        [Required(ErrorMessage = "Parcel Type Name is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
